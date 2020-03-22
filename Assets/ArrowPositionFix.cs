using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPositionFix : MonoBehaviour
{
    public float yPos;
    public GameObject dummy;
    // Start is called before the first frame update
    void Start()
    {
        yPos = transform.position.y;
        //dummy = GetComponentInParent<Transform>();
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = dummy.transform.position;
        
      
        transform.rotation = Quaternion.Euler(Vector3.forward);
        //print(transform.position);
        //print(dummy.transform.position);
    }
}
