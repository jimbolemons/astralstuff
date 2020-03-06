using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopeAnimsController : MonoBehaviour
{
    public GameObject animator;
    Animator anim;
    public bool run = false;
    public bool dead = false;
    public bool jump = false;
    public bool falling = false;
    public bool shoot = false;
    public bool dance = false;
    public bool climbing = false;
    public bool grounded = false;



    // Start is called before the first frame update
    void Start()
    {
        anim = animator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //this is a test
        if (Input.GetKey(KeyCode.E))
        {
           anim.SetBool("dance", true);

            // Debug.Log("HOLDING E");
        }
        else
        {
           anim.SetBool("dance", false);

            // Debug.Log("NO E");

        }
        //this is a test
        if (Input.GetKey(KeyCode.Q))
        {
            

            // Debug.Log("HOLDING E");
        }
        else
        {
           // anim.SetBool("dance", false);

            // Debug.Log("NO E");

        }


        //this is a test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("jump");

            // Debug.Log("HOLDING E");
        }
        else
        {
           // dance = false;
            // Debug.Log("NO E");

        }

        if (run)
        {
            // Debug.Log("Run");
            anim.SetBool("run", true);

        }
        else
        {
            //  Debug.Log("no Run");
            anim.SetBool("run", false);
        }

        if (dead)
        {
           // anim.SetBool("dead", true);
        }
        else
        {
           // anim.SetBool("dead", false);
        }


    }
    public void Climb()
    {
        anim.SetBool("climbing", true);
    }
    public void StopClimb()
    {
        anim.SetBool("climbing", false);
    }
}
