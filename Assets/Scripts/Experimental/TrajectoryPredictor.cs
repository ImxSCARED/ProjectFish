using System;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPredictor : MonoBehaviour
{
    #region Members
    LineRenderer trajectoryLine;
    [SerializeField, Range(10, 100), Tooltip("The maximum number of points the LineRenderer can have")]
    int maxPoints = 50;
    [SerializeField, Range(0.01f, 0.5f), Tooltip("The time increment used to calculate the trajectory")]
    float increment = 0.025f;
    [SerializeField, Range(1.05f, 2f), Tooltip("The raycast overlap between points in the trajectory, this is a multiplier of the length between points. 2 = twice as long")]
    float rayOverlap = 1.1f;
    #endregion

    public struct ProjectileProperties
    {
        public Vector3 direction;
        public Vector3 initialPosition;
        public float initialSpeed;
        public float mass;
        public float drag;
    }
    private void Start()
    {
        if (trajectoryLine == null)
            trajectoryLine = GetComponent<LineRenderer>();

        SetTrajectoryVisible(false);
    }

    public void PredictTrajectory(ProjectileProperties projectile)
    {
        Vector3 velocity = projectile.direction * (projectile.initialSpeed / projectile.mass);
        Vector3 position = projectile.initialPosition;
        Vector3 nextPosition;
        float overlap;

        UpdateLineRender(maxPoints, (0, position));

        for (int i = 1; i < maxPoints; i++)
        {
            // Estimate velocity and update next predicted position
            velocity = CalculateNewVelocity(velocity, projectile.drag, increment);
            nextPosition = position + velocity * increment;

            // Overlap rays by small margin to ensure it never misses a surface
            overlap = Vector3.Distance(position, nextPosition) * rayOverlap;

            //Stop updating our line when hitting surface
            if (Physics.Raycast(position, velocity.normalized, out RaycastHit hit, overlap))
            {
                UpdateLineRender(i, (i - 1, hit.point));
                break;
            }

            //If nothing is hit, continue rendering the arc
            position = nextPosition;
            UpdateLineRender(maxPoints, (i, position));
        }
    }
    /// <summary>
    /// Sets line count and an induvidual position at the same time
    /// </summary>
    /// <param name="count">Number of points in the line</param>
    /// <param name="pointPos">The position of an induvidual point</param>
    private void UpdateLineRender(int count, (int point, Vector3 pos) pointPos)
    {
        trajectoryLine.positionCount = count;
        trajectoryLine.SetPosition(pointPos.point, pointPos.pos);
    }

    private Vector3 CalculateNewVelocity(Vector3 velocity, float drag, float increment)
    {
        velocity += Physics.gravity * increment;
        velocity *= Mathf.Clamp01(1f - drag * increment);
        return velocity;
    }

    public void SetTrajectoryVisible(bool visible)
    {
        trajectoryLine.enabled = visible;
    }
}

