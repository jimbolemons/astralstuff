﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class handles the basic information for the player's movement and controls
/// Movement modules can inherit more information from this class
/// </summary>
public class BaseMovementModule : MonoBehaviour
{
    //reference to the charactercontroller component of the player
    public CharacterController controller;

    [Tooltip("How fast the player moves")]
    public float speed = 18;

    public float AirSpeed = 5f;

    [Tooltip("Max speed of the player")]
    public float maxSpeed = 25;
    //TODO: Check this is working correctly

    [Tooltip("How fast the player falls (should be negative value)")]
    public static float gravity = -35;


    [Tooltip("How much dead area before the control sticks cause movement")]
    public float deadZone = .2f;
    //TODO: apply this with controller support

    [Tooltip("How quickly the player slows down each frame (1 - value)")]
    [Range(0, 1)]
    public float friction = .8f;
    //TODO: Apply this better

    //How fast the playery was moving last frame
    [System.NonSerialized]
    public Vector3 pVelocity = new Vector3();
    //TODO: Apply this better

    [Tooltip("Amount of force applied when player jumps")]
    public float jumpSpeed = 10;

    //aka velocity. the direction the player is moving this frame
    [System.NonSerialized]
    public Vector3 direction;

    //ability to run off and still jump
    public float coyoteTime = .2f;
    public float coyoteTimer;
    
    //reference to the camera target assistant
    Transform cameraTarget;

    //the player's transform, for editing correctly
    public Transform player;
    // Start is called before the first frame update
    public GameObject playermodel;
    public float rotateSpeed;
    public Transform pivot;

    public bool canJump;

    float jumptimer = .1f;
    bool canJumpTimer = true;

    public float jumpExtentionTimerBase = .5f;
    public float jumpExtentionTimer;
    


    void Start()
    {
        coyoteTimer = coyoteTime;
    }

    // Update is called once per frame
    void Update()
    {
        pivot.transform.position = player.transform.position;
        //only run if game isn't paused
        if (!MasterStaticScript.gameIsPaused)
        {
            CheckForRotation();

            XYMovement();
            //print(direction);        
            //direction.y -= gravity * Time.deltaTime;


            //apply movement
            controller.Move(direction * Time.deltaTime);
            //TODO: Make this actually work better
            pVelocity = direction * (1 - friction);

            //apply friction
            if (Input.GetAxis("Vertical") <= deadZone && Input.GetAxis("Horizontal") <= deadZone)
            {
                controller.Move(pVelocity * Time.deltaTime);
            }


            //cleanup
            //if (controller.isGrounded) pVelocity.y = 0;
        }
    }
   

    virtual public void CheckForRotation()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            //player.transform.rotation = Quaternion.Euler(0, cameraTarget.rotation.eulerAngles.y, 0);
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
           // player.transform.rotation = Quaternion.Euler(0, cameraTarget.rotation.eulerAngles.y, 0);
        }
        if (Input.GetMouseButton(0))
        {
           // player.transform.rotation = Quaternion.Euler(0, cameraTarget.rotation.eulerAngles.y, 0);      
        }
    }

    virtual public void XYMovement()
    {
        //print(controller.isGrounded);
        if (controller.isGrounded)
        {
            direction = Input.GetAxis("Vertical") * transform.forward;
            //TODO: rotate player and move them forward
            direction += Input.GetAxis("Horizontal") * transform.right;
            direction *= speed;

            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
                Quaternion newrot = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
                playermodel.transform.rotation = Quaternion.Slerp(playermodel.transform.rotation, newrot, rotateSpeed * Time.deltaTime);
            }

           
           

            //put animations here i think

            //TODO: implement coyote time to prevent jump getting eaten
            if (Input.GetButton("Jump") && canJump && canJumpTimer)
            {
                Jump();
                canJumpTimer = false;
                jumpExtentionTimer = jumpExtentionTimerBase;
                jumptimer = .1f;
            }
            

            if (jumptimer > 0)
            {
                jumptimer -= Time.deltaTime;
            }
            else if (jumptimer < 0)
            {
                canJumpTimer = true;
            }
        }
        else
        {//if not grounded
            if (Input.GetButton("Jump") && jumpExtentionTimer > 0)
            {
                direction.y = jumpSpeed;
            }

            if (jumpExtentionTimer > 0) jumpExtentionTimer -= Time.deltaTime;

            //apply weaker input controls while in the air
            Vector3 airControl = new Vector3();

            airControl = Input.GetAxis("Vertical") * transform.forward;
            airControl += Input.GetAxis("Horizontal") * transform.right;
            airControl *= AirSpeed;

            airControl.y += gravity;

            direction += airControl * Time.deltaTime;
            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
                Quaternion newrot = Quaternion.LookRotation(new Vector3(airControl.x, 0f, airControl.z));
                playermodel.transform.rotation = Quaternion.Slerp(playermodel.transform.rotation, newrot, rotateSpeed * Time.deltaTime);
            }
           
        }
    }

    //apply jump value
    virtual public void Jump()
    {
        direction.y = jumpSpeed;
    }

    public void SetPlayer(Transform p)
    {
        player = p;
    }

    public void SetCharacterController(CharacterController c)
    {
        controller = c;
    }

    public void SetCameraTarget(Transform t)
    {
        cameraTarget = t;
    }
    
}

