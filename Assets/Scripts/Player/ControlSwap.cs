using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSwap : MonoBehaviour
{
    public GameObject hope;
    public GameObject dummy;
    Rigidbody dummyBody;
    public GameObject dummyArrow;

    public GameObject launchArc;
    public GameObject waffles;

    public BaseMovementModule hopeMovement;
    public BaseMovementModule wafflesMovement;

    public ThirdPersonCamera cameraControl;

    public FireDummyShootScript FireDummyShootScript;


    public int controlState = 0;

    public bool revertToBody;

    bool keyWasPressed;
    bool setupCheck = false;
    public AudioManager audio;
    public bool slow = false;

    public animsWaffles waff;

    public HopeAnimsController hoe;



    // Start is called before the first frame update
    void Start()
    {
        audio = FindObjectOfType<AudioManager>();
        FireDummyShootScript = GetComponent<FireDummyShootScript>();
        dummyBody = dummy.GetComponent<Rigidbody>();
        //hoe = GetComponentInChildren<HopeAnimsController>();
        // waff = GetComponentInChildren<animsWaffles>();


        launchArc.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //revertToBody = !Input.GetKey(KeyCode.LeftShift);
        if (!setupCheck && Time.time > .1f)
        {
            ActivateHope();
            setupCheck = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            slow = true;
            if (controlState == 0)
            {
                launchArc.SetActive(true);
            }

        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            slow = false;
            launchArc.SetActive(false);
        }

        if (controlState == 0)
        {
            if (slow)
            {
                Time.timeScale = 0.5f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
        if (controlState == 1)
        {
            if (!slow)
            {
                Time.timeScale = 1f;
            }
        }

        if (Input.GetKeyUp(KeyCode.Tab) && Input.GetKeyUp(KeyCode.Tab) != keyWasPressed)
        {
            if (FireDummyShootScript.canFire)
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
                    slow = false;

                    FireDummyShootScript.fireBlank();

                }

            }
        }
        //if key was pressed last frame
        keyWasPressed = Input.GetKeyDown(KeyCode.Tab);
    }

    void ActivateHope()
    {
        //SOUND
        audio.Play("switch");
        waff.run2 = true;
        FindObjectOfType<AudioManager>().Stop("wafflerun");
        hope.SetActive(true);
        waffles.SetActive(false);
        if (revertToBody)
        {
            transform.position = dummy.transform.position;
        }
        dummyBody.velocity = Vector3.zero;
        dummy.SetActive(false);
        dummyArrow.SetActive(false);
    }
    void ActivateWaffles()
    {
        //Sound
        audio.Play("switch");
        hoe.run2 = true;
        FindObjectOfType<AudioManager>().Stop("hoperun");
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
