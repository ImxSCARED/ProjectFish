using static FishProperties;
using UnityEngine;
using System.Collections.Generic;

public class Fish : MonoBehaviour
{
    [SerializeField] private FishProperties properties;
    public FishTier tier;

    public FishEverything theFish;

    
    [SerializeField] private float speed = 1;
    [SerializeField] private float width = 3;
    [SerializeField] private float height = 3;

    private float timeBeforeAction = 0;
    private float actionTime = 0;
    private Transform fishingHitboxNode;
    private void Start()
    {
        theFish = properties.GetFish(tier);
    }
    private void Update()
    {    
    }
    /// <summary>
    /// Based on tier, moves to a random node after a time limit to make fishing harder
    /// </summary>
    public void FishDash(Transform[] hitboxNodes, bool isFishBeingReeled)
    {
        timeBeforeAction += Time.deltaTime;
        switch (tier)
        {
            case FishTier.SMALL:
                if(timeBeforeAction > 2)
                {
                    if (!fishingHitboxNode)
                    {
                        fishingHitboxNode = hitboxNodes[Random.Range(0, (int)hitboxNodes.Length - 1)];
                    }
                    actionTime += Time.deltaTime;
                    if(actionTime < 1)
                    {
                        if (isFishBeingReeled)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, fishingHitboxNode.position, 3f * Time.deltaTime);
                        }
                        else
                        {
                            transform.position = Vector3.MoveTowards(transform.position, fishingHitboxNode.position, 5f * Time.deltaTime);
                        }
                        
                    }
                    else
                    {
                        actionTime = 0;
                        timeBeforeAction = 0;
                        fishingHitboxNode = null;
                    }
                }
                break;
            case FishTier.MEDIUM:
                if (timeBeforeAction > 2)
                {

                }
                break;
            case FishTier.LARGE:
                if (timeBeforeAction > 1)
                {

                }
                break;
        }
    }
}
