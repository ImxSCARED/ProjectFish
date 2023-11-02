using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // --EDITOR VARIABLES--
    // Scene objects
    [SerializeField]
    Rigidbody m_rigidbody;
    [SerializeField]
    BoatProperties m_boatProperties;

    // Core physics
    [SerializeField]
    Vector3 m_maxVelocity;

    // Buoyancy
    [SerializeField]
    Transform m_buoyancyPointsParent;
    [SerializeField]
    LayerMask m_waterLayer;

    // --CODE VARIABLES--
    // Core physics
    Vector3 m_lastPos;
    Vector3 m_velocity;

    // Buoyancy
    Transform[] m_buoyancyPoints;

    // Start is called before the first frame update
    void Start()
    {
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
        // Apply forces
        ApplyForce(Vector3.down, BoatMaths.m_gravity);

        EvaluateBuoyancy();

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

    private void EvaluateBuoyancy()
    {
        foreach (Transform point in m_buoyancyPoints)
        {
            RaycastHit hit;
            bool underwater = Physics.Raycast(point.position, Vector3.up, out hit, 10, m_waterLayer);

            if (underwater)
            {
                ApplyForce(Vector3.up, hit.distance + 0.5f);
                Debug.Log(point.name + " is underwater!");
            }
        }
    }
}
