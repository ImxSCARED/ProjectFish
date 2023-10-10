using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Fish Properties")]
public class FishProperties : ScriptableObject
{
    [System.Serializable]
    public struct FishEverything
    {
        public string name;
        public GameObject fishBody;
        FishTier tier;
        public GameObject fishIcon;
    }
    public enum FishTier { SMALL, MEDIUM, LARGE }
    public FishEverything[] smallFish;
    public FishEverything[] mediumFish;
    public FishEverything[] largeFish;
    public FishEverything GetFish(FishTier tier)
    {
        FishEverything fish;
        int randomNumber;
        switch (tier)
        {
            case FishTier.SMALL:
                randomNumber = Random.Range(0, smallFish.Length - 1);
                fish = smallFish[randomNumber];
                return fish;

            case FishTier.MEDIUM:
                randomNumber = Random.Range(0, mediumFish.Length - 1);
                fish = mediumFish[randomNumber];
                return fish;

            case FishTier.LARGE:
                randomNumber = Random.Range(0, largeFish.Length - 1);
                fish = largeFish[randomNumber];
                return fish;

            //to get the function to shutup about no return
            default:
                randomNumber = Random.Range(0, smallFish.Length - 1);
                fish = smallFish[randomNumber];
                return fish;
        }
    }

}
