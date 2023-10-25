using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Fishing : MonoBehaviour
{
    [SerializeField] private FishingHitbox fishingSpot;
    
    public CaptureCircle minigameBackground;
    public GameObject minigameMover;

    private Fish currentFish;
    public bool fishing;


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
        if (fishing)
        {
            currentFish.FishMinigame(fishingSpot.rightPoints, minigameBackground.fishInCircle);
            if (fishingSpot.currentFish == null)
            {
                Debug.Log("fish ran away");
                fishing = false;
                minigameMover.SetActive(false);
            }
        }
    }
    public void MoveMM(Vector2 input)
    {
        minigameMover.transform.position += minigameMover.transform.forward * (input.y * Time.deltaTime) * 5;
        minigameMover.transform.position += minigameMover.transform.right * (input.x * Time.deltaTime) * 5;
    }
}