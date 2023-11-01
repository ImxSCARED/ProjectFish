using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Fishing : MonoBehaviour
{
    [SerializeField] private FishingHitbox fishingSpot;
    
    public CaptureCircle minigameBackground;
    public GameObject minigameMover;
    public Bounds bounds;
    private Fish currentFish;
    public bool fishing;

    [Header("Exposed Variables")]
    public int maxHarpoons;
    public float fishWrangleSpeed;
    public float hasdhashdhasd;
    public void FishMinigame()
    {
        if (!fishing)
        {
            if (fishingSpot.currentFish)
            {
                fishing = true;
                minigameMover.SetActive(true);
                minigameMover.transform.position = fishingSpot.currentFish.transform.position;
                minigameBackground.fishTransform = fishingSpot.currentFish.transform;
                currentFish = fishingSpot.currentFish.GetComponent<Fish>();
            }
        }
    }

    public void FishCaught()
    {
        fishing = false;
        minigameMover.SetActive(false);
        GameObject fish = fishingSpot.currentFish;
        fish.SetActive(false);
    }
    private void Update()
    {
        bounds = fishingSpot.GetComponent<MeshCollider>().bounds;
        if (fishing)
        {
            if (fishingSpot.currentFish == null)
            {
                Debug.Log("fish ran away");
                fishing = false;
                minigameMover.SetActive(false);
                return;
            }
            //Checks whether the fish is on the left or rightside of the boat, then give the fish the relative nodes to dash to
            Vector3 perp = Vector3.Cross(transform.forward, fishingSpot.currentFish.transform.position - transform.position);
            float dir = Vector3.Dot(perp, transform.up);
            if (dir > 0f)
            {
                currentFish.FishDash(fishingSpot.rightPoints, minigameBackground.fishInCircle);
            }
            else
            {
                currentFish.FishDash(fishingSpot.leftPoints, minigameBackground.fishInCircle);
            }
        }
    }

    /// <summary>
    /// Moves catching circle based off input, then clamps it to the bounds of the fishing area
    /// </summary>
    /// <param name="input"></param>
    public void MoveMM(Vector2 input)
    {
        minigameMover.transform.position += minigameMover.transform.forward * (input.y * Time.deltaTime) * 5;
        minigameMover.transform.position += minigameMover.transform.right * (input.x * Time.deltaTime) * 5;
        //https://forum.unity.com/threads/finding-distance-to-edge-of-an-object.660157/ use this, check when yuo get out of fishingSpot zone, then do a raycast to the edge, get a distance and move back that much
    }
}