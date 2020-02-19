using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookRayCast : MonoBehaviour
{

    public GameObject hookHolder;
    public GameObject cameras;
    public GameObject player;
    public GameObject hook;
    public float playerTravelSpeed = 20f;
    public float ropeLength = 100f;
    public float hookDelay = 5f;

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
    bool unhookedButInAir = false;

    LineRenderer rope;

    // Start is called before the first frame update
    void Start()
    {
        rope = hook.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        DrawHookLine();
        layerMask = 1 << 2;
        layerMask = ~layerMask;
        //line2 = new Ray(hookHolder.transform.position, hookHolder.transform.forward);
        line = new Ray(cameras.transform.position, cameras.transform.forward);
        Timer();

        if (!hooked & fired)
        {
            FireHook();
            MakeLines();
        }

        if (hooked && fired)
        {
            ReelIn();
        }

        if (hooked && fired && ropeDis <= 1)
        {
           UnHook();            
        }

        if (unhookedButInAir & !IsGrounded.downHook)
        {
            if (!IsGrounded.up)
            {
                ClimbUp();
            }
            else
            {
                unhookedButInAir = false;
            }           

        }
        
        if (unhookedButInAir & IsGrounded.downHook)
        {
            unhookedButInAir = false;
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

        if (Input.GetMouseButton(0) && !fired && canShoot)
        {
            fired = true;
        }
    }

    private void ReelIn()
    {
        
        ropeDis = Vector3.Distance(player.transform.position, hook.transform.position);
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = Vector3.MoveTowards(player.transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);
        BaseMovementModule.gravity = 0;
    }

    private void ClimbUp()
    {      
        player.transform.Translate(Vector3.forward * Time.deltaTime * 13f);
        player.transform.Translate(Vector3.up * Time.deltaTime * 18f);
    }

    private void UnHook()
    {
        if (unhookedButInAir & IsGrounded.cannotClimb)
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
        player.GetComponent<CharacterController>().enabled = true;
        BaseMovementModule.gravity = -35;
       // hook.transform.position = hookHolder.transform.position;
        hook.transform.position = cameras.transform.position;

        DestroyLines();
        
    }

    private void FireHook()
    {
        if (Physics.Raycast(line, out hit, ropeLength, layerMask))
        {
            //Debug.Log(hit.collider.name);
            //hookedPos = hit.point; 
            if (hit.collider.gameObject.tag == "Hookable")
            {
                hook.transform.position = hit.point;
                ropeDis = Vector3.Distance(player.transform.position, hook.transform.position);
                hooked = true;
                canShoot = false;
            }
        }
    }

    private void DrawHookLine()
    {
        Debug.DrawRay(hookHolder.transform.position, hookHolder.transform.forward * ropeLength, Color.yellow, layerMask);
        Debug.DrawRay(cameras.transform.position, cameras.transform.forward * ropeLength, Color.yellow, layerMask);

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
}
