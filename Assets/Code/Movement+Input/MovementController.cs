using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // --EDITOR VARIABLES--
    // Generic
    [Header("Generic")]
    [SerializeField]
    Rigidbody m_rigidbody;
    [SerializeField]
    BoatProperties m_boatProperties;

    // Core physics
    [Header("Physics")]
    [SerializeField]
    Vector3 m_maxVelocity;
    [SerializeField]
    float m_airFriction;
    [SerializeField]
    float m_waterFriction;

    // Buoyancy
    [Header("Buoyancy")]
    [SerializeField]
    Transform m_waterTransform;
    [SerializeField]
    Transform m_buoyancyPointsParent;
    [SerializeField]
    MeshCollider m_hullCollider;
    [SerializeField]
    float m_displacementAmount;
    [SerializeField]
    float m_depthBeforeSubmerged;

    // --CODE VARIABLES--
    // Core physics
    Vector3 m_lastPos;
    Vector3 test;
    Vector3 m_velocity;

    Vector3 m_gravityForce;

    // Buoyancy
    Transform[] m_buoyancyPoints;

    Vector3 m_hullBounds;

    // Start is called before the first frame update
    void Start()
    {
        // Find approximate volume and surface area of ship
        // TODO: make methods to find (not approcimate) the volume and surface area of a mesh
        m_hullBounds = m_hullCollider.bounds.size;
        m_boatProperties.m_volume = m_hullBounds.x * m_hullBounds.y * m_hullBounds.z;
        m_boatProperties.m_surfaceArea = m_hullBounds.x + m_hullBounds.y + m_hullBounds.z;

        // Get all buoyancy points
        m_buoyancyPoints = new Transform[m_buoyancyPointsParent.childCount];
        for (int i = 0; i < m_buoyancyPointsParent.childCount; i++)
        {
            m_buoyancyPoints[i] = m_buoyancyPointsParent.GetChild(i);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Setup
        // Find force applied by gravity
        m_gravityForce = m_boatProperties.m_mass * BoatMaths.m_gravity * Vector3.down;

        // Apply friction
        ApplyFriction();

        // Apply forces
        ApplyForce(m_gravityForce);
        ApplyBuoyancy();

        // Clamp velocity
        m_velocity = BoatMaths.ClampVector(m_velocity, -m_maxVelocity, m_maxVelocity);


        // Apply velocity
        m_rigidbody.MovePosition(transform.position + m_velocity * Time.deltaTime);
    }

    void LateUpdate()
    {
        m_lastPos = transform.position;
    }

    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
        {
            m_velocity = Vector3.zero;

            transform.position = m_lastPos;
        }
    }*/

    public void ApplyForce(Vector3 force)
    {
        m_velocity += force * Time.deltaTime;
    }

    public void ApplyForce(Vector3 direction, float magnitude)
    {
        m_velocity += direction * magnitude * Time.deltaTime;
    }

    private void ApplyBuoyancy()
    {
        foreach (Transform point in m_buoyancyPoints)
        {
            if (point.position.y <= m_waterTransform.position.y)
            {
                float displacementMultiplier = Mathf.Clamp01(-(point.position.y - m_waterTransform.position.y) / m_depthBeforeSubmerged) * (m_displacementAmount / m_buoyancyPoints.Length);
                ApplyForce(BoatMaths.m_gravity * displacementMultiplier * Vector3.up);
            }
        }
    }

    private void ApplyFriction()
    {
        float percentSubmerged = FindPercentSubmerged(m_hullCollider.transform, m_hullBounds);
        Debug.Log(percentSubmerged);

        Vector3 waterFriction;
        waterFriction.x = (float)BoatMaths.CalculateDrag(m_hullBounds.x, m_velocity.x, BoatMaths.m_seaWaterKinematicViscosity,
                                                         BoatMaths.m_seaWaterDensity, m_boatProperties.m_surfaceArea);
        waterFriction.y = (float)BoatMaths.CalculateDrag(m_hullBounds.y, m_velocity.y, BoatMaths.m_seaWaterKinematicViscosity,
                                                         BoatMaths.m_seaWaterDensity, m_boatProperties.m_surfaceArea);
        waterFriction.z = (float)BoatMaths.CalculateDrag(m_hullBounds.z, m_velocity.z, BoatMaths.m_seaWaterKinematicViscosity,
                                                         BoatMaths.m_seaWaterDensity, m_boatProperties.m_surfaceArea);

        Vector3 airFriction;
        airFriction.x = (float)BoatMaths.CalculateDrag(m_hullBounds.x, m_velocity.x, BoatMaths.m_airKinematicViscosity,
                                                       BoatMaths.m_airDensity, m_boatProperties.m_surfaceArea) * (1 - percentSubmerged);
        airFriction.y = (float)BoatMaths.CalculateDrag(m_hullBounds.y, m_velocity.y, BoatMaths.m_airKinematicViscosity,
                                                       BoatMaths.m_airDensity, m_boatProperties.m_surfaceArea) * (1 - percentSubmerged);
        airFriction.z = (float)BoatMaths.CalculateDrag(m_hullBounds.z, m_velocity.z, BoatMaths.m_airKinematicViscosity,
                                                       BoatMaths.m_airDensity, m_boatProperties.m_surfaceArea);

        m_velocity = BoatMaths.ApplyVector3Friction(m_velocity, ((waterFriction * percentSubmerged) * m_waterFriction) + ((airFriction * (1 - percentSubmerged)) * m_airFriction));
    }

    private void ApplyFriction2()
    {
        float percentSubmerged = FindPercentSubmerged(m_hullCollider.transform, m_hullBounds);
        Debug.Log(percentSubmerged);

        m_velocity = BoatMaths.ApplyFrictionToVector3(m_velocity, (m_waterFriction * percentSubmerged) + (m_airFriction * (1 - percentSubmerged)));
    }

    // TODO: this only works well with a vertical ship. needs to be updated to account for rotations
    private float FindPercentSubmerged(Transform point, Vector3 size)
    {
        float distanceFromWater = point.position.y - m_waterTransform.position.y;
        return Mathf.Clamp(-(distanceFromWater - (size.y / 2) / size.y), 0, 1);
    }
}
