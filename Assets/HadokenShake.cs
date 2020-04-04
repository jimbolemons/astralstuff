using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadokenShake : MonoBehaviour
{
    public float max = 3;
    public float min = -3;
    public float shakeSpeed = 50;    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float position = Mathf.Lerp(max, min, Mathf.PingPong(Time.time * shakeSpeed, 1));
        transform.position = new Vector3(transform.position.x + position, transform.position.y, transform.position.z); 
    }
}
