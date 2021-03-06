﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HookRayCast : MonoBehaviour
{

    public GameObject hookHolder;
    public GameObject cameraPivot;
    GameObject player;
    public GameObject hook;
    public GameObject climbDetector;

    public HopeAnimsController hopeAnims;
    public float playerTravelSpeed = 20f;
    public float ropeLength = 100f;
    public float hookDelay = 5f;

    public float upAmount = 20;
    public float forwardAmount = 15;

    
    public float climbDelaybase = .5f;
    float climbDelay;

    public BaseMovementModule playerMove;

    Ray line;
    RaycastHit hit;
    Vector3 hookedPos;
    int layerMask = 0;
    float ropeDis = 50f;
    float timer = 0f;
    bool fired = false;
    bool hooked = false;
    bool timerRunning = true;
    bool canShoot = true;
    bool canHit;
    bool unhookedButInAir = false;
    bool climbing = false;

    LineRenderer rope;
    Image img;

    // Start is called before the first frame update
    void Start()
    {
        rope = hook.GetComponent<LineRenderer>();
        img = GameObject.Find("crosshair").GetComponent<Image>();
        hopeAnims = gameObject.GetComponentInChildren<HopeAnimsController>();
        climbDelay = climbDelaybase;

        player = MasterStaticScript.playerReference;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!MasterStaticScript.gameIsPaused)
        {
            Climb2();
            TestLine();
            //DrawHookLine();
            layerMask = 1 << 2;
            layerMask = ~layerMask;
            //line2 = new Ray(hookHolder.transform.position, hookHolder.transform.forward);
            line = new Ray(cameraPivot.transform.position, cameraPivot.transform.forward);
            Timer();

            if (!hooked & fired)
            {
                FireHook();

            }

            if (hooked && fired)
            {

                MakeLines();
                ReelIn();
            }

            if (hooked && fired && ropeDis <= 5)
            {
                UnHook();

            }
            if (player.GetComponent<ControlSwap>().controlState != 0)
            {
                UnHook();
            }

            if (unhookedButInAir && !IsGrounded.downHook && !IsGrounded.up)
            {
                if (!CanClimb.cannotClimb)
                {
                    //ClimbUp();
                    //BaseMovementModule.gravity = -35;
                    climbing = true;
                   
                }
                else
                {
                   
                    unhookedButInAir = false;
                    climbing = false;
                    
                }

            }
            if (IsGrounded.downHook && IsGrounded.up)
            {
                climbing = false;
            }

            
            

            if (unhookedButInAir && IsGrounded.downHook)
            {
                unhookedButInAir = false;
            }
        }
    }

    private void Timer()
    {
        if (timerRunning)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            timerRunning = false;
            timer = hookDelay;
            canShoot = true;
        }

        if (Input.GetMouseButton(0) && !fired && canShoot && canHit)
        {
            fired = true;
        }
    }

    private void ReelIn()
    {
        hopeAnims.hooked = true;
        ropeDis = Vector3.Distance(player.transform.position, hook.transform.position);
        player.GetComponent<CharacterController>().enabled = false;
        BaseMovementModule.gravity = 0;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerMove.direction = Vector3.zero;
        player.transform.position = Vector3.MoveTowards(player.transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);
    }

    private void ClimbUp()
    {
        print("Trying to climb");
        
        if (climbDelay > 0)
        {            
            //player.transform.Translate(Vector3.up * Time.deltaTime * upAmount);
            //player.transform.Translate(climbDetector.transform.forward * Time.deltaTime * forwardAmount);
           
            playerMove.direction = Vector3.zero;
        }
        else
        {
            unhookedButInAir = false;
            climbDelay = climbDelaybase;
        }
        climbDelay -= Time.deltaTime;

    }
    private void Climb2()
    {
        if(climbing)
        {
            // player.GetComponent<Rigidbody>().AddForce(Vector3.up*upAmount, ForceMode.Force);
          //  player.GetComponent<Rigidbody>().AddForce(Vector3.forward*forwardAmount,ForceMode.Force) ;
        climbing = false;
       player.transform.Translate(Vector3.up * upAmount);
        player.transform.Translate(climbDetector.transform.forward * forwardAmount);
        }

    }

    private void UnHook()
    {
        if (unhookedButInAir & CanClimb.cannotClimb && player.GetComponent<ControlSwap>().controlState == 0 )
        {
            unhookedButInAir = false;
        }
        else
        {
            unhookedButInAir = true;
        }
        timerRunning = true;
        hooked = false;
        fired = false;
        climbing = false;

        player.GetComponent<CharacterController>().enabled = true;
        BaseMovementModule.gravity = -35;
       // hook.transform.position = hookHolder.transform.position;
        hook.transform.position = cameraPivot.transform.position;
        hopeAnims.hooked = false;        

        DestroyLines();        
    }

    private void TestLine()
    {
        if (Physics.Raycast(line, out hit, ropeLength, layerMask))
        {
            if (hit.collider.gameObject.tag == "Hookable")
            {
                img.color = UnityEngine.Color.green;
                canHit = true;
            }
            else
            {
                img.color = UnityEngine.Color.red;
                canHit = false;
            }

        }
        else
        {
            img.color = UnityEngine.Color.red;
            canHit = false;
        }
    } 

    private void FireHook()
    {
        if (Physics.Raycast(line, out hit, ropeLength, layerMask))
        {
            //Debug.Log(hit.collider.name);
            //hookedPos = hit.point; 
            if (hit.collider.gameObject.tag == "Hookable")
            {
                
                FindObjectOfType<AudioManager>().Play("hook");
                hook.transform.position = hit.point;
                ropeDis = Vector3.Distance(player.transform.position, hook.transform.position);
                hooked = true;
                canShoot = false;
            }
            else
            {
                fired = false;
                
            }
        }
        
    }

    private void DrawHookLine()
    {
        //Debug.DrawRay(hookHolder.transform.position, hookHolder.transform.forward * ropeLength, Color.yellow, layerMask);
        //Debug.DrawRay(cameras.transform.position, cameras.transform.forward * ropeLength, Color.yellow, layerMask);

    }

    private void MakeLines()
    {
        rope.SetVertexCount(2);
        rope.SetPosition(0, hookHolder.transform.position);
        rope.SetPosition(1, hook.transform.position);
    }

    private void DestroyLines()
    {
        rope.SetVertexCount(0);
    }
    public void WafflesUnhook()
    {
        if (hooked)
        {
           
            UnHook();
            //TODO: need to send waffles twards the grapling hook point

        }
    }
}
