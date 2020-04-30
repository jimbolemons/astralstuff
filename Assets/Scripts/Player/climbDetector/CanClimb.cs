using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanClimb : MonoBehaviour
{
    int layerMask = 0;
    public GameObject player;
    public GameObject climbSpot;

    public static bool cannotClimb;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        layerMask = 1 << 2;
        layerMask = ~layerMask;
        cannotClimb = CannotClimb();
        Debug.DrawRay(climbSpot.transform.position, transform.forward * 2f, Color.yellow, layerMask);

    }
    public bool CannotClimb()
    {
        return Physics.Raycast(climbSpot.transform.position, transform.forward, 2f, layerMask);
    }
}
