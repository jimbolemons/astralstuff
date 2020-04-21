using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyVelocityTracker : MonoBehaviour
{
    public Rigidbody body;
    // Update is called once per frame

    private void Update()
    {
        Debug.Log(body.velocity);
    }
    private void OnDisable()
    {
        Debug.Log("dummy body being disabled");
        //body.velocity = Vector3.zero;
    }
}
