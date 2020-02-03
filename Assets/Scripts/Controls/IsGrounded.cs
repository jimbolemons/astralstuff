using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    public GameObject player;
    public static bool Grounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Grounded = IsGroundeded();
        
    }
    public  bool IsGroundeded()
    {
        return Physics.Raycast(player.transform.position, -transform.up, 1.5f);
    }

}
