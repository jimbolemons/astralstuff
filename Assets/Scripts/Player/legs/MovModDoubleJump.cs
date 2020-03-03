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


    public GameObject animator;
    Animator anim;
  
    float runAnim;


    void Start()
    {
        //initialize default count
        doubleJumpCount = doubleJumpBase;
        anim = animator.GetComponent<Animator>();
        
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
                    if (IsGrounded.wall)
                    {
                        Jump();
                        doubleJumpCount--;
                    }
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

                /*  if (Input.GetAxis("Vertical") > 0)
                  {
                      runAnim = 1;
                  }
                  else if (Input.GetAxis("Vertical") < 0)
                  {
                      runAnim = -1;
                  }
                  else
                  {

                      if (runAnim < .1f && runAnim > -.1f)
                      {
                          runAnim = 0;
                      }
                      else
                      {
                          runAnim = runAnim / 1.1f;
                      }
                  }*/

               
            }
            else {
                
            }


            
            
                anim.SetFloat("MoveSpeed", Mathf.Abs(Mathf.Abs( direction.x) + Mathf.Abs(direction.z)));
            

            //cleanup
            //if (controller.isGrounded) velocityY = 0;
        }
    }
}
