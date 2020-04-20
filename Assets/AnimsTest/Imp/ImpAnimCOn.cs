using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpAnimCOn : MonoBehaviour
{
    public GameObject animator;
    Animator anim;
    public bool run = false;
    public bool dead = false;

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
           
           // Debug.Log("HOLDING E");
           anim.SetTrigger("hit");
        }
        else
        {
           // Debug.Log("NO E");
            
        }
        
        

        if (run)
        {
           // Debug.Log("Run");
            anim.SetBool("walking", true);

        }
        else
        {
          //  Debug.Log("no Run");
            anim.SetBool("walking", false);
        }

        if (dead)
        {
            anim.SetBool("dead", true);
        }
        else
        {
            anim.SetBool("dead", false);
        }
        if (!MasterStaticScript.gameIsPaused)
        {

            anim.enabled = true;
        }
        else
        {
            anim.enabled = false;

        }

    }
    public void Hit ()
    {
        anim.SetTrigger("hit");

    }
}
