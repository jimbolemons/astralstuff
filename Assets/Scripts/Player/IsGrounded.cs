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

    public GameObject animator;
    Animator anim;
    public GameObject animator2;
    Animator anim2;

    public static bool up;
    public static bool down;
    public bool wasGrounded;
    public static bool downHook;
    public static bool right;
    public static bool left;
    public static bool front;
    public static bool back;
    public static bool wall;
    //public static bool cannotClimb;
    public static bool canClimb;

    public ParticleSystem landingDust;

    int layerMask = 0;

    private void Start()
    {
        anim = animator.GetComponent<Animator>();
        anim2 = animator2.GetComponent<Animator>();
    }
    void Update()
    {

        layerMask = 1 << 2;
        layerMask = ~layerMask;
        up = Up();
        down = Down();
        downHook = DownHook();
        right = Right();
        left = Left();
        front = Front();
        back = Back();
        wall = TouchingWall();
        //cannotClimb = CannotClimb();
        //Debug.DrawRay(climbSpot.transform.position, transform.forward * 2f, Color.yellow, layerMask);


        Debug.DrawRay(player.transform.position, transform.up * 1.5f, Color.yellow, layerMask);
        Debug.DrawRay(player.transform.position, -transform.up * 1.5f, Color.yellow, layerMask);
        Debug.DrawRay(player.transform.position, transform.right * 1f, Color.yellow, layerMask);
        Debug.DrawRay(player.transform.position, -transform.right * 1f, Color.yellow, layerMask);
        Debug.DrawRay(player.transform.position, transform.forward * 1f, Color.yellow, layerMask);
        Debug.DrawRay(player.transform.position, -transform.forward * 1f, Color.yellow, layerMask);
        AnimateFunc();

        wasGrounded = down;
    }
    public void AnimateFunc()
    {
        if (down)
        {
            if (wasGrounded != down)
            {
                //print("just grounded");
                landingDust.Play();
            }
            anim.SetBool("Grounded", true);
            anim2.SetBool("Grounded", true);
        }
        if (!down)
        {
            anim.SetBool("Grounded", false);
            anim2.SetBool("Grounded", false);
        }

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
    public bool Down()
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
    //public bool CannotClimb()
    // {
    //    return Physics.Raycast(climbSpot.transform.position, transform.forward, 2f, layerMask);
    //}


}
