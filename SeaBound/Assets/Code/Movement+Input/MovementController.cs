using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // --EDITOR VARIABLES--
    [SerializeField]
    float m_maxForwardVelocity;
    [SerializeField]
    float m_maxBackwardVelocity;
    [SerializeField]
    float m_speed;
    [SerializeField]
    float m_turnRate;
    [SerializeField]
    float m_friction;
    [SerializeField]
    Rigidbody m_rigidbody;

    // --CODE VARIABLES--
    Quaternion m_rotation;
    float m_rotationEuler;

    float m_movementDirection; // 1D direction (forward or backward) of movement (-1 to 1)
    float m_maxVelocity; // actual max velocity, depending on if we're moiving forward or backward

    // --UNITY METHODS--
    void Awake()
    {
        //m_rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // --GLOBAL VARIABLES--
        // direction forward or backward
        m_movementDirection = Mathf.Sign(Vector3.Dot(m_rigidbody.velocity, transform.forward));
        m_maxVelocity = m_movementDirection > 0 ? m_maxForwardVelocity : m_maxBackwardVelocity;

        // --FRICTION--
        // if friction would cause us to shoot past 0, just set velocity to 0
        if (m_rigidbody.velocity.magnitude - (m_friction * Time.deltaTime) < 0)
        {
            m_rigidbody.velocity = Vector3.zero;
        }
        else
        {
            m_rigidbody.AddForce(-m_friction * m_movementDirection * transform.forward);
        }
    }

    // --PUBLIC METHODS--
    // TODO: Figure out why this breaks when going backwards
    /// <summary>
    /// Adds some velocity, controlled by m_speed, to the object's current velocity, without exceeding the velocity cap.
    /// </summary>
    /// <param name="magnitude">The amount to multiply the velocity added by.</param>
    public void AddVelocity(float magnitude = 1)
    {
        float velocity = magnitude * m_speed;

        float amountOverMax = m_rigidbody.velocity.magnitude + Mathf.Abs(velocity) - (m_maxVelocity * Mathf.Abs(magnitude));
        if (amountOverMax > 0)
        {
            velocity -= amountOverMax * Mathf.Sign(magnitude);
        }

        m_rigidbody.AddForce(velocity * transform.forward);
    }

    /// <summary>
    /// Turns the object via rotation.
    /// </summary>
    /// <param name="angle">How sharply the boat should turn (-1 to 1).</param>
    public void Turn(float angle)
    {
        float eulerTurn = angle * m_turnRate * Time.deltaTime;
        Quaternion turn = Quaternion.Euler(0, eulerTurn, 0);

        m_rotationEuler += eulerTurn;
        m_rotation = Quaternion.Euler(0, m_rotationEuler, 0);

        m_rigidbody.MoveRotation(m_rotation);

        m_rigidbody.velocity = turn * m_rigidbody.velocity;
    }
}
