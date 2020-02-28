using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToCamera : MonoBehaviour
{
    public Transform cameras; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0)       {

           //transform.LookAt(cameras);
           transform.rotation = Quaternion.Euler(0, cameras.rotation.eulerAngles.y, 0);     
        }

    }
}
