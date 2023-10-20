using static FishProperties;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private FishProperties properties;
    [SerializeField] private FishingHitbox hitbox;
    public FishTier tier;

    public FishEverything theFish;

    
    [SerializeField] private float speed = 1;
    [SerializeField] private float width = 3;
    [SerializeField] private float height = 3;

    private float timeBeforeAction = 0;
    private void Start()
    {
        theFish = properties.GetFish(tier);
    }
    private void Update()
    {    
    }

    public void FishMinigame()
    {
        timeBeforeAction += Time.deltaTime;
        switch (tier)
        {
            case FishTier.SMALL:
                if(timeBeforeAction < 3)
                {

                }
                else
                {
                    timeBeforeAction = 0;
                }
                break;
            case FishTier.MEDIUM:
                if (timeBeforeAction < 2)
                {

                }
                else
                {
                    timeBeforeAction = 0;
                }
                break;
            case FishTier.LARGE:
                if (timeBeforeAction < 1)
                {

                }
                else
                {
                    timeBeforeAction = 0;
                }
                break;
        }
    }
}
