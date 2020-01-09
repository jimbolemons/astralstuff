using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This module enables the player to double jump
/// Inherits base stats from BaseMovementModule class
/// </summary>
public class MovModDoubleJump : BaseMovementModule
{
    [Tooltip("How many midair jumps")]
    public int doubleJumpBase = 1;
    int doubleJumpCount;
    
    void Start()
    {
        //initialize default count
        doubleJumpCount = doubleJumpBase;
    }

    // Update is called once per frame
    void Update()
    {
        //only run if game isn't paused
        if (!MasterStaticScript.gameIsPaused)
        {
            CheckForRotation();

            XYMovement();

            //custom code for this inherited class
            if (controller.isGrounded)
            {
                doubleJumpCount = doubleJumpBase;
            }
            else
            {                
                if(doubleJumpCount > 0  && Input.GetButtonDown("Jump"))
                {
                Jump();
                doubleJumpCount--;
                }
            }


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
            //if (controller.isGrounded) velocityY = 0;
        }
    }
}
