using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMaths
{
    public static float m_gravity = 9.81f;     // In m/s
    public static float m_waterDensity = 997;  // In kg/m^3

    public static Vector3 ClampVector(Vector3 vector, Vector3 min, Vector3 max)
    {
        vector.x = Mathf.Clamp(vector.x, min.x, max.x);
        vector.y = Mathf.Clamp(vector.y, min.y, max.y);
        vector.z = Mathf.Clamp(vector.z, min.z, max.z);

        return vector;
    }
}
