using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public class MovementController : MonoBehaviour
{
    // --EDITOR VARIABLES--
    [Header("Object References")]
    [SerializeField]
    Rigidbody m_rigidbody;
    [SerializeField]
    Transform m_rotationAxis;

    [Header("Player Control")]

    // Acceleration
    [SerializeField]
    [Tooltip("Maximum force that the engine can impart on the player")]
    [Min(0f)]
    float m_engineForce;
    [SerializeField]
    [Tooltip("Speed at which the engine's power increases per second in percentages (e.g. 200% per second)")]
    [Min(0f)]
    float m_accelerationSpeed;

    // Turning
    [SerializeField]
    [Tooltip("Rate at which ship turning accelerates per second in degrees. Increase to account for turn friction")]
    [Min(0f)]
    float m_turnSpeed;

    [Header("Forces")]

    // Turning
    [SerializeField]
    [Tooltip("Amount of velocity maintained in the forward direction when turning, in percentages")]
    [Range(0f, 100f)]
    float m_turningVelocityRetained;

    // Friction
    [SerializeField]
    [Tooltip("Rate at which the boat slows in the horizontal directions")]
    [Min(0f)]
    float m_sideFriction;
    [SerializeField]
    [Tooltip("Friction applied to the ship's turning")]
    [Min(0f)]
    float m_turnFriction;

    // --CODE VARIABLES--

    // Velocity
    Vector3 m_velocity;

    // Engine
    float m_enginePower; // Percentage value, from 0% - 100%

    void FixedUpdate()
    {
        ApplyEngine();

        // Clamp velocity
        m_velocity = ClampMagnitude3(m_velocity, -m_engineForce * m_enginePower, m_engineForce * m_enginePower);

        // Apply friction
        ApplyFriction();

        // Apply rotation
        

        m_rigidbody.MovePosition(transform.position + m_velocity * Time.deltaTime);
    }

    /// <summary>
    /// Causes the engine to accelerate by magnitude * m_accelerationSpeed
    /// </summary>
    /// <param name="magnitude">Amount from -1 to 1 that the engine should accelerate</param>
    public void Accelerate(float magnitude)
    {
        magnitude = Mathf.Clamp(magnitude, -1f, 1f);

        float deltaEngine = magnitude * m_accelerationSpeed * Time.deltaTime;
        m_enginePower += deltaEngine;

        m_enginePower = Mathf.Clamp(m_enginePower, 0f, 100f);
    }

    /// <summary>
    /// Rotates the boat by magnitude * m_turnSpeed
    /// TODO: make this rotate around custom axis
    /// TODO: ensure the turning works properly with speed
    /// </summary>
    /// <param name="magnitude">Amount from -1 to 1 that the boat should rotate</param>
    public void Turn(float magnitude)
    {
        magnitude = Mathf.Clamp(magnitude, -1f, 1f);

        float deltaTurn = magnitude * m_turnSpeed * Mathf.Lerp(0, 1, m_enginePower/100);

        m_rigidbody.MoveRotation(m_rigidbody.rotation * Quaternion.Euler(0f, deltaTurn * Mathf.Deg2Rad, 0));

        float decimalPercentTVR = m_turningVelocityRetained / 100;
        m_velocity = (transform.forward * m_velocity.magnitude * decimalPercentTVR) + (m_velocity * (1 - decimalPercentTVR));
    }

    /// <summary>
    /// Adds a vector3 force to velocity, mostly used internally
    /// </summary>
    /// <param name="force">Force to apply</param>
    public void AddForce(Vector3 force)
    {
        m_velocity += force * Time.deltaTime;
    }

    /// <summary>
    /// Set the forward velocity to the engine's force
    /// </summary>
    private void ApplyEngine()
    {
        Vector3 localVelocity = transform.InverseTransformVector(m_velocity);
        localVelocity.z = (m_enginePower / 100) * m_engineForce;

        m_velocity = transform.TransformVector(localVelocity);
    }

    /// <summary>
    /// Apply sideways friction to velocity - forward friction is not needed
    /// </summary>
    private void ApplyFriction()
    {
        Vector3 localVelocity = transform.InverseTransformVector(m_velocity);

        float sideFriction = m_sideFriction * Time.deltaTime;

        if (Mathf.Abs(localVelocity.x) - sideFriction < 0)
        {
            localVelocity.x = 0;
        }
        else
        {
            localVelocity.x -= sideFriction * Mathf.Sign(localVelocity.x);
        }

        m_velocity = transform.TransformVector(localVelocity);
    }

    /// <summary>
    /// Clamp a vector3 based on its magnitude
    /// </summary>
    /// <param name="vector">Vector3 to clamp</param>
    /// <param name="maxMagnitude">Maximum magnitude before the vector is clamped</param>
    /// <returns></returns>
    private Vector3 ClampMagnitude3(Vector3 vector, float minMagnitude, float maxMagnitude)
    {
        if (vector.magnitude > maxMagnitude)
        {
            Vector3 deltaForce = vector - (vector.normalized * maxMagnitude);
            return vector - deltaForce;
        }
        if (vector.magnitude < minMagnitude)
        {
            Vector3 deltaForce = vector - (vector.normalized * minMagnitude);
            return vector - deltaForce;
        }

        return vector;
    }
}
