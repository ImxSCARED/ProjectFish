using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{
    public int Money = 0;
    public int smallFishValue;
    public int mediumFishValue;
    public int largeFishValue;

    private bool isDocked = false;
    private List<FishProperties.FishData> storedFish = new List<FishProperties.FishData>();
    public void AddFish(FishProperties.FishData caughtFish)
    {
        storedFish.Add(caughtFish);
    }
    public void Dock()
    {
        GetComponent<InputManager>().ChangeActionMap("UI");
        isDocked = true;

    }

    public void SellFish()
    {
        foreach(FishProperties.FishData fish in storedFish)
        {
            switch (fish.tier)
            {
                case FishProperties.FishTier.SMALL:
                    Money += smallFishValue;
                    break;
                case FishProperties.FishTier.MEDIUM:
                    Money += mediumFishValue;
                    break;
                case FishProperties.FishTier.LARGE:
                    Money += largeFishValue;
                    break;
            }
        }
        storedFish.Clear();
    }
}
