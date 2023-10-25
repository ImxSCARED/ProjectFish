using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingHitbox : MonoBehaviour
{
    public GameObject currentFish = null;
    public Transform[] leftPoints;
    public Transform[] rightPoints;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Fish")
        {
            currentFish = collision.gameObject;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (currentFish == collision.gameObject)
        {
            currentFish = null;
        }
    }
}
