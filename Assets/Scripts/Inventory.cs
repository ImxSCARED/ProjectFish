using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<FishProperties.FishEverything> storedFish;
    [SerializeField] private PlayerManager m_playerManager;
    public void AddFish(FishProperties.FishEverything collectedFish)
    {
        storedFish.Add(collectedFish);
        m_playerManager.OnFishAdd(collectedFish);
    }
}
