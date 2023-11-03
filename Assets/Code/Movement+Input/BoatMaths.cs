using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMaths
{
    public static float m_gravity = 9.81f;                          // In m/s

    public static float m_seaWaterDensity = 1027;                   // In kg/m^3
    public static float m_airDensity = 1.2041f;

    public static double m_seaWaterKinematicViscosity = 1.04e-6d;   // In m^2/s
    public static double m_airKinematicViscosity = 1.48e-5d;   // In m^2/s

    /// <summary>
    /// Clamps the x, y, and z floats of a vector between the respective x, y, and z floats of min and max
    /// </summary>
    /// <param name="vector">Vector to clamp</param>
    /// <param name="min">Minimum values vector</param>
    /// <param name="max">Maximum values vector</param>
    /// <returns>Clamped vector</returns>
    public static Vector3 ClampVector(Vector3 vector, Vector3 min, Vector3 max)
    {
        vector.x = Mathf.Clamp(vector.x, min.x, max.x);
        vector.y = Mathf.Clamp(vector.y, min.y, max.y);
        vector.z = Mathf.Clamp(vector.z, min.z, max.z);

        return vector;
    }

    /// <summary>
    /// Brings a value closer to 0 by some amount determined by friction
    /// </summary>
    /// <param name="initialSpeed">The speed to apply friction to</param>
    /// <param name="friction">The amount of friction to apply to initialSpeed</param>
    /// <returns>The speed after friction has been applied</returns>
    public static float ApplyFriction(float initialSpeed, float friction)
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
    }

    /// <summary>
    /// Brings each value of a Vector3 closer to 0 by some amount determined by friction
    /// </summary>
    /// <param name="initialVelocity">The velocity to apply friction to</param>
    /// <param name="friction">The amount of friction to apply to initialVelocity</param>
    /// <returns>The velocity after friction has been applied</returns>
    public static Vector3 ApplyFrictionToVector3(Vector3 initialVelocity, float friction)
    {
        initialVelocity.x = ApplyFriction(initialVelocity.x, friction);
        initialVelocity.y = ApplyFriction(initialVelocity.y, friction);
        initialVelocity.z = ApplyFriction(initialVelocity.z, friction);

        return initialVelocity;
    }

    /// <summary>
    /// Brings each value of a Vector3 closer to 0 by some amount determined by the respective values of a friction Vector3
    /// </summary>
    /// <param name="initialVelocity">The velocity to apply friction to</param>
    /// <param name="friction">The amount of friction to apply to initialVelocity</param>
    /// <returns>The velocity after friction has been applied</returns>
    public static Vector3 ApplyVector3Friction(Vector3 initialVelocity, Vector3 friction)
    {
        initialVelocity.x = ApplyFriction(initialVelocity.x, friction.x);
        initialVelocity.y = ApplyFriction(initialVelocity.y, friction.y);
        initialVelocity.z = ApplyFriction(initialVelocity.z, friction.z);

        return initialVelocity;
    }

    /// <summary>
    /// Calculates Reynolds number for the given inputs
    /// </summary>
    /// <param name="length">Length of the submerged object</param>
    /// <param name="velocity">Velocity of the submerged object</param>
    /// <param name="kinematicViscosity">Viscosity of the fluid</param>
    /// <returns>Reynolds number as a float</returns>
    public static double ReynoldsNumber(float length, float velocity, double kinematicViscosity)
    {
        return (length * Mathf.Abs(velocity)) / kinematicViscosity;
    }

    /// <summary>
    /// Calculates the drag coefficient for a ship, based on ITTC 1957
    /// </summary>
    /// <param name="reynoldsNum">Reynolds number for the ship in sea water</param>
    /// <returns>Drag coefficient as a double</returns>
    public static double DragCoefficient(double reynoldsNum)
    {
        return 0.075d / Math.Pow(Math.Log(reynoldsNum) - 2, 2);
    }

    /// <summary>
    /// Calculates the drag coefficient for a ship, based on ITTC 1957
    /// </summary>
    /// <param name="length">Length of the submerged object</param>
    /// <param name="velocity">Velocity of the submerged object</param>
    /// <param name="kinematicViscosity">Viscosity of the fluid</param>
    /// <returns>Drag coefficient as a double</returns>
    public static double DragCoefficient(float length, float velocity, double kinematicViscosity)
    {
        return 0.075d / Math.Pow(Math.Log(ReynoldsNumber(length, velocity, kinematicViscosity)) - 2, 2);
    }

    public static double CalculateDrag(double dragCoefficient, float fluidDensity, float velocity, float areaInFluid)
    {
        return 0.5 * fluidDensity * Mathf.Pow(velocity, 2) * areaInFluid * dragCoefficient;
    }

    public static double CalculateDrag(float length, float velocity, double kinematicViscosity, float fluidDensity, float areaInFluid)
    {
        return 0.5 * fluidDensity * Mathf.Pow(velocity, 2) * areaInFluid * DragCoefficient(length, velocity, kinematicViscosity);
    }

    public static Vector3 CalculateBuoyancyForce(float submergedVolume)
    {
        float massDisplaced = submergedVolume * m_seaWaterDensity;

        return massDisplaced * m_gravity * Vector3.up;
    }
}
