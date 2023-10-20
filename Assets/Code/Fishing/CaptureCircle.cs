using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureCircle : MonoBehaviour
{
    [SerializeField] private Transform shipTransform;
    public Transform fishTransform;


    private bool fishInCircle;

    private void Update()
    {
        if (fishTransform)
        {
            if (fishInCircle)
            {
                fishTransform.position = Vector3.MoveTowards(fishTransform.position, shipTransform.position, 2f * Time.deltaTime);
            }
            else
            {
                fishTransform.position = Vector3.MoveTowards(fishTransform.position, shipTransform.position, -1.5f * Time.deltaTime);
            }
        }
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Fish")
        {
            fishInCircle = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if(collision.tag == "Fish")
        {
            fishInCircle = false;
        }
    }
}
