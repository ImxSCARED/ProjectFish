using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Fishing : MonoBehaviour
{
    [SerializeField] private FishingHitbox fishingSpot;
    
    public CaptureCircle minigameBackground;
    public GameObject minigameMover;

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
        minigameMover.transform.position += new Vector3((input.x * Time.deltaTime) * 5, 0, (input.y * Time.deltaTime) * 5);
    }
}