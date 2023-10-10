using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public HorizontalLayoutGroup horizontalLayoutGroup;

    public void OnFishAdd(FishProperties.FishEverything storedFish)
    {
        Instantiate(storedFish.fishIcon, horizontalLayoutGroup.transform, false);
    }
}
