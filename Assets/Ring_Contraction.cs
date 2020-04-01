using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring_Contraction : MonoBehaviour
{
    public float initialSize;
    float sizeTimer;
    public float secondsToShrink;
    // Start is called before the first frame update
    void Start()
    {
        sizeTimer = secondsToShrink;
        transform.localScale = new Vector3(initialSize, 1, initialSize);        
    }

    // Update is called once per frame
    void Update()
    {
        float size = Mathf.Lerp(1, initialSize, sizeTimer / secondsToShrink);
        //print(size);
        transform.localScale = new Vector3(size,1,size);
        //print(transform.localScale);
        //print(Mathf.Lerp(1, initialSize, sizeTimer/initialSize));
        sizeTimer -= Time.deltaTime;
        if(sizeTimer < 0)
        {
            Destroy(gameObject);
        }
    }
}
