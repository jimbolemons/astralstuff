using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private Vector3 scale = new Vector3(.5f,.5f,.5f);
    private Vector3 scale2;
    public GameObject hook;
    public GameObject hookHolder;
    public GameObject player;

    public float hookTravelSpeed;
    public float playerTravelSpeed;

    public static bool fired;
    public static bool reelIn;
    public bool hooked;
    public GameObject hookedObj;

    public float maxDistance;
    private float ropeLength;
    private float ropeDis;

    
   
    bool swinging;
    Vector3 newVel;
  
    float disToGround;

 

   
    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            ReturnHook();
            fired = true;
        }
        // TODO want to check to see if the player is grounded here as well
        if (Input.GetButton("Jump") && hooked)
        {            
            ReturnHook();
        }
    }

    private void Update()
    {
        hook.gameObject.transform.localScale = hook.gameObject.transform.localScale;
        if (Input.GetMouseButton(1) && gameObject.activeSelf)
            ReturnHook();

        // fireing the hok
        if (fired)
        {
            LineRenderer rope = hook.GetComponent<LineRenderer>();
            rope.SetVertexCount(2);
            rope.SetPosition(0, hookHolder.transform.position);
            rope.SetPosition(1, hook.transform.position);
        }

        //reel in to hok
        if (Input.GetKey(KeyCode.R) && hooked)
        {
            reelIn = true;
        }
        //if the hook is fired and is also not hooked into somthing do this
        if (fired == true && hooked == false)
        {
            hook.transform.parent = null;
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            ropeLength = Vector3.Distance(player.transform.position, hook.transform.position);

            if (ropeLength >= maxDistance)
                ReturnHook();
        }
        // Debug.Log(currentDistance);
        //if the hook is hooked into somthing do this
        if (hooked == true && fired == true)
        {
            hook.transform.parent = hookedObj.transform;           
            hook.transform.SetParent(hookedObj.transform, true);

            Vector3 v = player.transform.position - hook.transform.position;

            float dis = v.magnitude;
            newVel = v;

            Vector3 myUp = (player.transform.position - hook.transform.position).normalized;

            // this casuses the player to swing on the rope
            if (!reelIn && dis > ropeLength)
            {
                swinging = true;
                newVel.Normalize();
                v = Vector3.ClampMagnitude(v, ropeLength);
                player.transform.position = hook.transform.position + v;            
               
            }

            // this checks to see what is below the player and then does absolutly notthing about it at the moment
            if (Physics.Raycast(player.transform.position, Vector3.down, disToGround + 1))
            {
                
            }

            float distanceToHook = Vector3.Distance(player.transform.position, hook.transform.position);

            

            //climb up the rope
            if (Input.GetKey(KeyCode.E))
            {
                ropeLength -= 5 * Time.deltaTime;
            }

            //climb down the rope
            if (Input.GetKey(KeyCode.Q))
            {
                ropeLength += 5 * Time.deltaTime;
            }


                //if reeling in do this
                if (reelIn)
                {
                    player.GetComponent<CharacterController>().enabled = false;
                player.transform.position = Vector3.MoveTowards(player.transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);                
                    BaseMovementModule.gravity = 0;               
                }




                    //Debug.Log(Input.GetAxis("Horizontal"));
                    //Debug.Log(distanceToHook);
                    //if hope is close to the hook stop her from going in any farther
                    

                        // if hope reels in and then presses any controls she will resles 
                        if ( reelIn && distanceToHook < 2)
                        {
                
                            player.GetComponent<CharacterController>().enabled = true;
                            
                            ropeLength = 2;
                            if (Input.GetButton("Jump"))
                            {                   
                                ReturnHook();
                                reelIn = false;
                            }
                            if (Input.GetAxis("Horizontal") >= .1f || Input.GetAxis("Horizontal") <= -.1f)
                            {
                                ReturnHook();
                                reelIn = false;
                            }
                            if (Input.GetAxis("Vertical") >= .1f || Input.GetAxis("Vertical") <= -.1f)
                            {
                                ReturnHook();
                                reelIn = false;
                            }
                         
                         }
                        else if (distanceToHook < 2)
                        {
                            ropeLength += 1 *Time.deltaTime;
                        }

        } else {
            // if the hook is not fired it resets its positionn and scale
            if (!fired)
            {
                hook.transform.SetParent(hookHolder.transform, true);
                hook.transform.localPosition = Vector3.zero;
                hook.transform.localScale = new Vector3(.5f, .5f, .5f);
            }                   
            
            player.GetComponent<Rigidbody>().useGravity = true;
        }
        
    }


    //resets hook into players gun
    void ReturnHook()
    {
        player.GetComponent<CharacterController>().enabled = true;

        hook.transform.SetParent(hookHolder.transform, true);
        hook.transform.localPosition = Vector3.zero;
        hook.transform.localScale = new Vector3(.5f, .5f, .5f);

        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;       

        fired = false;
        reelIn = false;
        hooked = false;

        LineRenderer rope = hook.GetComponent<LineRenderer>();
        rope.SetVertexCount(0);
        BaseMovementModule.gravity = -35;
    }


    private void FixedUpdate()
    {
        //if the player is not on the ground and they are swinging and they are not above the position of the hook then do this
        if (!IsGrounded() && swinging && player.transform.position.y <= hook.transform.position.y)
        {
            Debug.Log("i would like to swing now please");

        }
    }



    bool IsGrounded()
    {
        return Physics.Raycast(player.transform.position, -transform.up, disToGround + 1f);
    }

}
