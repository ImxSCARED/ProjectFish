using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{
    [SerializeField] private ConeHitbox lCannonRange;
    [SerializeField] private ConeHitbox rCannonRange;

    [SerializeField] private Transform lCannonBallStart;
    [SerializeField] private Transform rCannonBallStart;
    [SerializeField] private GameObject cannonBall;
    private Rigidbody projecileRB;
    public void FireCannons()
    {
        GameObject currentProjectile = Instantiate(cannonBall);
        cannonBall.transform.position = lCannonBallStart.position;

        projecileRB = cannonBall.GetComponent<Rigidbody>();
        projecileRB.AddForce((lCannonBallStart.position - lCannonRange.EnemiesInRange[0].transform.position) * 30, ForceMode.Impulse);

    }
}
