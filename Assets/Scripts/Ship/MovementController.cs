using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // EDITOR VARIABLES
    [SerializeField]
    float m_maxForwardVelocity;
    [SerializeField]
    float m_speed;
    [SerializeField]
    float m_turnRate;

    // CODE VARIABLES
    Rigidbody m_rigidbody;

    Quaternion m_rotation;
    float m_rotationEuler;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // TODO: this can surely be made more efficient
        if (Vector3.Dot(m_rigidbody.velocity, transform.forward) > 0)
        {
            float amountOverMax = m_rigidbody.velocity.magnitude - m_maxForwardVelocity;
            if (amountOverMax > 0)
            {
                m_rigidbody.AddForce(-amountOverMax * transform.forward);
            }
        }
        else if (Vector3.Dot(m_rigidbody.velocity, transform.forward) < 0)
        {
            float amountOverMax = m_rigidbody.velocity.magnitude - m_maxForwardVelocity;
            if (amountOverMax > 0)
            {
                m_rigidbody.AddForce(amountOverMax * transform.forward);
            }
        }
    }

    // PUBLIC METHODS
    public void AddVelocity(float magnitude = 1)
    {
        m_rigidbody.AddForce(magnitude * m_speed * transform.forward);
    }

    /// <summary>
    /// Turn the boat via rotation
    /// </summary>
    /// <param name="angle">How sharply the boat should turn (-1 to 1)</param>
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
