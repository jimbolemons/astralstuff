using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToCamera : MonoBehaviour
{
    public Transform camera; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {

            transform.LookAt(camera);
        }

    }
}
