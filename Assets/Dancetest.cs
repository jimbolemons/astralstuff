using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dancetest : MonoBehaviour
{
    public GameObject animator2;
    Animator anim2;

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

    }
}
