using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanClimb : MonoBehaviour
{
   public static bool frontClimb;
    int layerMask = 0;
    public GameObject climbSpot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        layerMask = 1 << 2;
        layerMask = ~layerMask;
        frontClimb = FrontClimb();
        Debug.DrawRay(climbSpot.transform.position, transform.forward * 2f, Color.yellow, layerMask);
        Debug.Log(frontClimb);
    }
    public bool FrontClimb()
    {
        return Physics.Raycast(climbSpot.transform.position, transform.forward, 2f, layerMask);
    }
}
