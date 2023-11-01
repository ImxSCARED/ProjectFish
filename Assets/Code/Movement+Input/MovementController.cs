using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // --EDITOR VARIABLES--
    [SerializeField]
    Rigidbody m_rigidbody;
    [SerializeField]
    MeshCollider m_hullCollider;
    [SerializeField]
    float m_maxVelocity;
    [SerializeField]
    float m_speed;
    [SerializeField]
    float m_brakeSpeed;
    [SerializeField]
    float m_mass;                           // In kilograms
    [SerializeField]
    float m_maxRotationAxisDisplacement;    // In metres
    [SerializeField]
    float m_turnResistance;
    [SerializeField]
    float m_rudderDragCoefficient;
    [SerializeField]
    float m_waterDensity;
    [SerializeField]
    float m_rudderSize;

    // --CODE VARIABLES--
    Vector2 m_velocity;
    float m_rotationalVelocity;

    float m_acceleration;
    float m_rotationalAcceleration;

    float m_rotationAxisDisplacement;

    float m_centroidRotationalInertia;      // Required to calculate rotational inertia around some axis not in the centre
    float m_rotationalInertia;

    float m_rudderAngle;                    // In radians
    float m_heading;                        // In radians


    // --UNITY METHODS--
    void Awake()
    {
        m_centroidRotationalInertia = (1/12) * m_mass * (m_hullCollider.bounds.size.x * m_hullCollider.bounds.size.x +          // Ic = (1/12)m(l^2 + w^2)
                                                         m_hullCollider.bounds.size.z * m_hullCollider.bounds.size.z);

    }

    void FixedUpdate()
    {
        // --VELOCITY CAP--
        // Cap movement velocity
        // Accounts for diagonal movement
        if (m_velocity.magnitude > m_maxVelocity)
        {
            m_velocity.x = m_velocity.normalized.x * m_maxVelocity;
            m_velocity.y = m_velocity.normalized.y * m_maxVelocity;
        }

        // --SET TURNING AXIS--
        m_rotationAxisDisplacement = Mathf.Lerp(0, m_maxRotationAxisDisplacement, m_velocity.magnitude / m_maxVelocity);

        m_rotationalInertia = m_centroidRotationalInertia + m_mass * (m_rotationAxisDisplacement * m_rotationAxisDisplacement); // I = Ic + md^2


    }

    // --PUBLIC METHODS--
    /// <summary>
    /// Increases the boat's acceleration.
    /// </summary>
    /// <param name="magnitude">The magnitude by which speed is applied to the player's acceleration.</param>
    public void Accelerate(float magnitude = 1)
    {
        float acceleration = magnitude * (magnitude > 0 ? m_speed : -m_brakeSpeed);

        velocity *= Time.deltaTime;
        if (m_velocity.magnitude + (velocity * Time.deltaTime) < 0)
        {
            velocity = 0;
        }

        Vector2 forward2D = new(transform.forward.x, transform.forward.z);
        m_velocity += velocity * forward2D;
    }

    /*/// <summary>
    /// Adds some velocity, controlled by the player's speed, to the object's current turning velocity.
    /// </summary>
    /// <param name="magnitude">How sharply the boat should turn (-1 to 1).</param>
    public void Turn(float magnitude)
    {
        float eulerTurn = magnitude * m_turnRate * Time.deltaTime;

        m_rotationalVelocity += eulerTurn;
    }

    /// <summary>
    /// Brings a value closer to 0 by some amount determined by friction
    /// </summary>
    /// <param name="initialSpeed">The speed before friction is applied</param>
    /// <param name="friction">The amount to apply to initialSpeed</param>
    /// <returns>The speed after friction is applied</returns>
    private float ApplyFriction(float initialSpeed, float friction)
    {
        // Sign agnostic - if positive, this will subtract toward 0, if negative, it will add toward 0
        if (Mathf.Abs(initialSpeed) - (friction * Time.deltaTime) <= 0)
        {
            return 0;
        }
        else
        {
            return initialSpeed - Mathf.Sign(initialSpeed) * friction * Time.deltaTime;
        }
    }*/
}
