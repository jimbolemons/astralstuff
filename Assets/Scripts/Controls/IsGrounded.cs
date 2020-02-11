using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    /*
     * ..... this checks to see if the player is standing on something 
     */
    public GameObject player;
    public GameObject climbSpot;

    public static bool up;
    public static bool down;
    public static bool downHook;
    public static bool right;
    public static bool left;
    public static bool front;
    public static bool back;
    public static bool wall;
    public static bool cannotClimb;

    int layerMask = 0;

    void Update()
    {
        layerMask = 1 << 2;
        layerMask = ~layerMask;
        up    = Up();
        down  = Down();
        downHook = DownHook();
        right = Right();
        left  = Left();
        front = Front();
        back  = Back();
        wall = TouchingWall();
        cannotClimb = CannotClimb();
        Debug.DrawRay(climbSpot.transform.position, transform.forward * 2f, Color.yellow, layerMask);
        
        Debug.DrawRay(player.transform.position, transform.up* 1.5f, Color.yellow, layerMask);
        Debug.DrawRay(player.transform.position, -transform.up* 1.5f, Color.yellow, layerMask);
        Debug.DrawRay(player.transform.position, transform.right * 1f, Color.yellow, layerMask);
        Debug.DrawRay(player.transform.position, -transform.right * 1f, Color.yellow, layerMask);
        Debug.DrawRay(player.transform.position, transform.forward * 1f, Color.yellow, layerMask);
        Debug.DrawRay(player.transform.position, -transform.forward * 1f, Color.yellow, layerMask);
    }
    public bool TouchingWall()
    {
        if (left || right || front || back)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool Up()
    {        
        return Physics.Raycast(player.transform.position, transform.up, 1.5f, layerMask);        
    }
    public  bool Down()
    {
        return Physics.Raycast(player.transform.position, -transform.up, 1.5f, layerMask);
    }
    public bool DownHook()
    {
        return Physics.Raycast(player.transform.position, -transform.up, 5f, layerMask);
    }
    public bool Right()
    {
        return Physics.Raycast(player.transform.position, transform.right, 1f, layerMask);
    }
    public bool Left()
    {
        return Physics.Raycast(player.transform.position, -transform.right, 1f, layerMask);
    }
    public bool Front()
    {
        return Physics.Raycast(player.transform.position, transform.forward, 1f, layerMask);
    }
    public bool Back()
    {
        return Physics.Raycast(player.transform.position, -transform.forward, 1f, layerMask);
    }
    public bool CannotClimb()
    {
        return Physics.Raycast(climbSpot.transform.position, transform.forward, 2f, layerMask);
    }

}
