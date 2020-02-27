using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSwap : MonoBehaviour
{
    public GameObject hope;
    public GameObject body;
    public GameObject waffles;

    public int controlState = 0;

    public bool revertToBody;

    bool keyWasPressed;
    bool setupCheck = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!setupCheck && Time.time > .1f)
        {
            ActivateHope();
            setupCheck = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKeyDown(KeyCode.Tab)!= keyWasPressed){
            if(controlState == 0)
            {
                //state 0 - controlling hope
                ActivateWaffles();
                controlState = 1;
            } else
            if(controlState == 1)
            {
                ActivateHope();
                //state 1 controlling waffles
                controlState = 0;
            }
        }
        //if key was pressed last frame
        keyWasPressed = Input.GetKeyDown(KeyCode.Tab);
    }

    void ActivateHope()
    {
        hope.SetActive(true);
        waffles.SetActive(false);
        if (revertToBody)
        {
        transform.position = body.transform.position;
        }
        body.SetActive(false);        
    }
    void ActivateWaffles()
    {
        waffles.SetActive(true);
        hope.SetActive(false);
        body.SetActive(true);
        body.transform.position = transform.position;
        body.transform.SetParent(null);
        BaseMovementModule.gravity = -35;
    }
}
