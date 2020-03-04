using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dancetest : MonoBehaviour
{
    public GameObject animator2;
    Animator anim2;
    public bool run = false;

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
            Debug.Log("daadadad");
        }
        else
        {
            Debug.Log("no Dance");
            anim2.SetBool("dance", false);
        }
        //this is a test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim2.SetTrigger("Jump");
            
            
        }
        else
        {
            
            
           
        }

        if (run)
        {
            Debug.Log("Run");
            anim2.SetBool("running", true);

        }
        else
        {
            Debug.Log("no Run");
            anim2.SetBool("running", false);
        }

    }
}
