using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Firing : MonoBehaviour
{
    [SerializeField] private ConeHitbox lCannonRange;
    [SerializeField] private ConeHitbox rCannonRange;

    [SerializeField] private Transform lCannonBallStart;
    [SerializeField] private Transform rCannonBallStart;
    [SerializeField] private GameObject cannonBall;

    [SerializeField] private float force;
    public void FireCannons()
    {
        if (lCannonRange.EnemiesInRange.Count > 0)
        {
            GameObject currentProjectile = Instantiate(cannonBall, lCannonBallStart.position, Quaternion.identity);
            Vector3 direction = lCannonRange.EnemiesInRange[0].transform.position - lCannonBallStart.position;
            direction.Normalize();
            currentProjectile.GetComponent<Projectile>().Spawn(direction, force);
        }
        if (rCannonRange.EnemiesInRange.Count > 0)
        {
            GameObject currentProjectile = Instantiate(cannonBall, rCannonBallStart.position, Quaternion.identity);
            Vector3 direction = rCannonRange.EnemiesInRange[0].transform.position - rCannonBallStart.position;
            direction.Normalize();
            currentProjectile.GetComponent<Projectile>().Spawn(direction, force);
        }
    }
}