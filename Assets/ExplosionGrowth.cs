using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionGrowth : MonoBehaviour
{
    public float baseSize;
    public float growthSpeed;
    public float maxSize;
    public float growthMultiplier;
    public float growthNumber = 0;
    public float timeToGrow;

    public float timeToLinger;


    void Start()
    {
        transform.localScale = Vector3.one * baseSize;
        maxSize = baseSize * growthMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        growthNumber += Time.deltaTime * growthSpeed;

        transform.localScale = Vector3.one * Mathf.Lerp(baseSize, maxSize, growthNumber / timeToGrow);
        if(transform.localScale.y >= maxSize)
        {
            //print("big enough, time to stop");
            timeToLinger -= Time.deltaTime;
            if(timeToLinger <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
