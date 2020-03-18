using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killAfterTime : MonoBehaviour
{
    public float HowLongToLive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HowLongToLive -= Time.deltaTime;
        if (HowLongToLive <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}
