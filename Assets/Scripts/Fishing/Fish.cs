using static FishProperties;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private FishProperties properties;
    public FishTier tier;
    public GameObject shadow;

    public FishEverything theFish;

    private float timeCoutner = 0;
    [SerializeField] private float speed = 1;
    [SerializeField] private float width = 3;
    [SerializeField] private float height = 3;

    private Vector3 originalPosition;
    private float x;
    private float y;
    private float z;
    private void Start()
    {
        theFish = properties.GetFish(tier);
        originalPosition = transform.position;
    }
    public void InitiateInOriginalPosition()
    {
        shadow.gameObject.SetActive(true);
        shadow.transform.position = originalPosition;
        timeCoutner = 0; x = 0; y = 0; z = 0;
    }
    private void Update()
    {
        timeCoutner += Time.deltaTime * speed;
        z = Mathf.Cos(timeCoutner) * width;
        x = Mathf.Sin(timeCoutner) * height;

        transform.position = new Vector3(x, y, z) + originalPosition;    
    }
}
