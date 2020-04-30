using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animsWaffles : MonoBehaviour
{
    public GameObject animator2;
    Animator anim2;
    public bool run = false;
   public  bool run2;

    // Start is called before the first frame update
    void Start()
    {
        anim2 = animator2.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //this is a test
        if (Input.GetKey(KeyCode.E))
        {
             anim2.SetBool("dance", true);
            // Debug.Log("daadadad");
        }
        else
        {
             // Debug.Log("no Dance");
           anim2.SetBool("dance", false);
        }
        //this is a test
        if (Input.GetKeyDown(KeyCode.Space))
        {          
        

        }
        else
        {



        }
        


        if (run && IsGrounded.down)
        {
            //Debug.Log("Run");
            anim2.SetBool("running", true);
            if(run2 )
            {
            FindObjectOfType<AudioManager>().Play("wafflerun");
            run2 = false;
            }

        }
        else
        {
            // Debug.Log("no Run");
            anim2.SetBool("running", false);
             FindObjectOfType<AudioManager>().Stop("wafflerun");
            run2 = true;
        }
        if (!MasterStaticScript.gameIsPaused)
        {

            anim2.enabled = true;
        }
        else
        {
            anim2.enabled = false;

        }

    }
    public void Right()
    {
        anim2.SetTrigger("right");
    }
    public void Left()
    {
        anim2.SetTrigger("left");
    }
    public void Hit() 
    {
     anim2.SetTrigger("hit");
    }
}
