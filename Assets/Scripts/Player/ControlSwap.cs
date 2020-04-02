using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSwap : MonoBehaviour
{
    public GameObject hope;
    public GameObject dummy;
    public GameObject dummyArrow;
    public GameObject waffles;

    public BaseMovementModule hopeMovement;
    public BaseMovementModule wafflesMovement;

    public ThirdPersonCamera cameraControl;

    public Gun FireDummyShootScript;
    

    public int controlState = 0;

    public bool revertToBody;

    bool keyWasPressed;
    bool setupCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        FireDummyShootScript = GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!setupCheck && Time.time > .1f)
        {
            ActivateHope();
            setupCheck = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKeyDown(KeyCode.Tab) != keyWasPressed)
        {
            if (controlState == 0)
            {
                Vector3 velocity = hopeMovement.pVelocity;
                //state 0 - controlling hope
                ActivateWaffles();
                wafflesMovement.pVelocity = velocity;
                controlState = 1;
            }
            else
            if (controlState == 1)
            {   
                ActivateHope();
                //state 1 controlling waffles
                controlState = 0;
                cameraControl.SetLerp(true);
            }
        }
        //if key was pressed last frame
        keyWasPressed = Input.GetKeyDown(KeyCode.Tab);
    }

    void ActivateHope()
    {

        //SOUND
        FindObjectOfType<AudioManager>().Play("timeScream");
        hope.SetActive(true);
        waffles.SetActive(false);
        if (revertToBody)
        {
            transform.position = dummy.transform.position;
        }
        dummy.SetActive(false);
        dummyArrow.SetActive(false);
    }
    void ActivateWaffles()
    {
        //Sound
        FindObjectOfType<AudioManager>().Play("timeScream");
        hope.GetComponent<HookRayCast>().WafflesUnhook();
        waffles.SetActive(true);
        hope.SetActive(false);
        dummy.SetActive(true);
        dummyArrow.SetActive(true);
        //body.transform.position = transform.position;
        FireDummyShootScript.Fire();
        dummy.transform.SetParent(null);
        BaseMovementModule.gravity = -35;
    }
}
