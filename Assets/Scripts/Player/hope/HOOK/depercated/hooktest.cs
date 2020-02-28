using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hooktest : MonoBehaviour
{
    
    public GameObject hookHolder;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        layerMask = 1 << 2;
        layerMask = ~layerMask;       
        line = new Ray(hookHolder.transform.position, hookHolder.transform.forward);



        Timer();

        if (!hooked & fired)
        {
            FireHook();
        }
        if (hooked && fired)
        {
            ReelIn();
        }
        if (hooked && fired && ropeDis <= 1)
        {
            UnHook();
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
    private void UnHook()
    {
        timerRunning = true;
        hooked = false;
        fired = false;
        player.GetComponent<CharacterController>().enabled = true;
        BaseMovementModule.gravity = -35;
        hook.transform.position = hookHolder.transform.position;

    }
    private void FireHook()
    {
        if (Physics.Raycast(line, out hit, ropeLength, layerMask))
        {
            Debug.Log(hit.collider.name);
            //hookedPos = hit.point;                
            hook.transform.position = hit.point;
            ropeDis = Vector3.Distance(player.transform.position, hook.transform.position);
            hooked = true;
            canShoot = false;
        }
    }
    private void DrawHookLine()
    {
        Debug.DrawRay(hookHolder.transform.position, hookHolder.transform.forward * ropeLength, Color.yellow, layerMask);
    }
}
