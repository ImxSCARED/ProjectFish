using UnityEngine;

public class Waves : MonoBehaviour
{
    public float min = 2f;
    public float max = 3f;
    
    void Start()
    {

        min = transform.localEulerAngles.x - 3;
        max = transform.localEulerAngles.x + 3;

    }
    void Update()
    {

        transform.position += new Vector3(1,0,0) * Time.deltaTime;
        transform.localEulerAngles = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.localEulerAngles.y, transform.localEulerAngles.z);

    }
}
