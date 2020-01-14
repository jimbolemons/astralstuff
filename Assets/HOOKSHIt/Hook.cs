using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private Vector3 scale = new Vector3(.5f,.5f,.5f);
    private Vector3 scale2;
    public GameObject hook;
    public GameObject hookHolder;

    public float hookTravelSpeed;
    public float playerTravelSpeed;

    public static bool fired;
    public static bool reelIn;
    public bool hooked;
    public GameObject hookedObj;

    public float maxDistance;
    private float ropeLength;
    private float ropeDis;

    //public float  = 0f;
    Collider col;
    bool swinging;

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0) && fired == false)
        {
            fired = true;
        }
    }

    private void Update()
    {
        hook.gameObject.transform.localScale = hook.gameObject.transform.localScale;

        // fireing the hok
        if (fired)
        {
            LineRenderer rope = hook.GetComponent<LineRenderer>();
            rope.SetVertexCount(2);
            rope.SetPosition(0, hookHolder.transform.position);
            rope.SetPosition(1, hook.transform.position);
        }

        //reel in to hok
        if (Input.GetMouseButton(0) && hooked)
        {
            reelIn = true;
        }
        //if the hook is fired and is also not hooked into somthing do this
        if (fired == true && hooked == false)
        {
            hook.transform.parent = null;
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            ropeLength = Vector3.Distance(transform.position, hook.transform.position);

            if (ropeLength >= maxDistance)
                ReturnHook();
        }
        // Debug.Log(currentDistance);
        //if the hook is hooked into somthing do this
        if (hooked == true && fired == true)
        {
            hook.transform.parent = hookedObj.transform;           
            hook.transform.SetParent(hookedObj.transform, true);

            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);
            //if reeling in do this
            if (Input.GetKey(KeyCode.E))
            {
                ropeLength -= 5 * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                ropeLength += 5 * Time.deltaTime;
            }
            if (reelIn)
            { 
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);
                //TODO turn off gravity for the player so that the grapple works more smoothly
                //this.GetComponent<Rigidbody>().useGravity = false;
                BaseMovementModule.gravity = 0;
               // this.GetComponent<MovModDoubleJump>().gravity = 0;
            }
            // if not reeling in do this
            if (!reelIn && distanceToHook >= ropeLength)
            {
                BaseMovementModule.gravity = -20;
                transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * 36 );                
               // transform.position = ;
            }
            else
            {
               // BaseMovementModule.gravity = -35;
            }

            //Debug.Log(distanceToHook);
            if (distanceToHook < 2)
            {
                reelIn = false;
                ropeLength = 2;
                if (Input.GetButton("Jump"))
                {
                    ReturnHook();
                }
            }            

        } else {

            if (!fired)
            {
                hook.transform.SetParent(hookHolder.transform, true);
                hook.transform.localPosition = Vector3.zero;
                hook.transform.localScale = new Vector3(.5f, .5f, .5f);
            }
                       
            //TODO turn on gravity
            this.GetComponent<Rigidbody>().useGravity = true;
        }
        if (Input.GetMouseButton(1))
            ReturnHook();
    }
    //resets hook into players gun
    void ReturnHook()
    {
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

}
