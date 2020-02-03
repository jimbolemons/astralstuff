using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    /*
     * ..... this checks to see if the player is standing on something 
     */
    public GameObject player;
    public static bool Grounded;       
    void Update()
    {
        Grounded = IsGroundeded();
        
    }
    public  bool IsGroundeded()
    {
        return Physics.Raycast(player.transform.position, -transform.up, 1.5f);
    }

}
