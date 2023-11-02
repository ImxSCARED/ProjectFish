using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatProperties : MonoBehaviour
{
    // --EDITOR VARIABLES--

    [SerializeField]
    public float m_mass;                           // In kilograms
    [SerializeField]
    public float m_rudderArea;                     // In metres^2
    [SerializeField]
    public float m_maxRudderAngle = 35;            // In degrees

    // --CODE VARIABLES--

    // Static variables
    static Vector2 m_forward2D;

    static readonly float m_divisionFactor = 100;       // This is to counteract the fact that time.deltaTime makes numbers real small, so they're hard to manipulate in-editor

    // Private variables
    Vector2 m_velocity;
    float m_rotationalVelocity;

    float m_acceleration;
    float m_rotationalAcceleration;

    float m_pivotDisplacement;

    float m_centroidRotationalInertia;      // Required to calculate rotational inertia around some axis not in the centre
    float m_rotationalInertia;

    float m_rudderAngle;                    // In radians
    float m_heading;                        // In radians

    float m_realMaxVelocity;
    float m_realSpeed;
    float m_realBrakeSpeed;
}
