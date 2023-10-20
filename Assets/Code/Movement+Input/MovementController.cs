using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // --EDITOR VARIABLES--
    [SerializeField]
    float m_maxVelocity;
    [SerializeField]
    float m_speed;
    [SerializeField]
    float m_brakeSpeed;
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
    float m_brakeVelocity; // actual max velocity, depending on if we're moiving forward or backward

    float m_velocity;

    // --UNITY METHODS--
    void Awake()
    {

    }

    void FixedUpdate()
    {
        // --VELOCITY CAP--
        if (m_velocity > m_maxVelocity)
        {
            m_velocity = m_maxVelocity;
        }

        // --FRICTION--
        // if friction would cause us to shoot past 0, just set velocity to 0
        if (m_velocity - (m_friction * Time.deltaTime) < 0)
        {
            m_velocity = 0;
        }
        else
        {
            m_velocity -= m_friction * Time.deltaTime;
        }
    }

    // --PUBLIC METHODS--
    /// <summary>
    /// Adds some velocity, controlled by the player's speed, to the object's current velocity.
    /// </summary>
    /// <param name="magnitude">The force by which speed is applied to the player's velocity.</param>
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

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
