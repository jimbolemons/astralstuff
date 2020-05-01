using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HookStates : MonoBehaviour
{

    public GameObject hookHolder;
    public GameObject cameraPivot;
    GameObject player;
    public GameObject hook;
    public GameObject climbDetector;

    CharacterController playerController;
    Rigidbody playerBody;

    HopeAnimsController hopeAnims;
    public BaseMovementModule playerMove;

    [Space(10)]
    public float playerTravelSpeed = 20f;
    public float ropeLength = 100f;

    public float ropeDistance = 50;

    //public float climbSpeed;


    

    Ray line;
    RaycastHit hit;
    Vector3 hookedPos;
    int layerMask = 0;
    float ropeDis = 50f;


    public bool canHit;

    [Space(10)]
    public float howLongToClimbFor = .5f;
    float climbTimer;

    public float playerState = 1;

    Vector3 targetClimbPosition;
    Vector3 startClimbPosition;

    public float verticalOffset;
    public float forwardOffset;

    public bool useHelperCube = false;

    [Tooltip("Shows the target point, 'where player wants to climb to'")]
    //remove when fixed
    public GameObject helperCube;

    LineRenderer rope;
    Image img;

    // Start is called before the first frame update
    void Start()
    {
        player = MasterStaticScript.playerReference;
        playerController = player.GetComponent<CharacterController>();
        playerBody = player.GetComponent<Rigidbody>();

        rope = hook.GetComponent<LineRenderer>();
        img = GameObject.Find("crosshair").GetComponent<Image>();
        hopeAnims = gameObject.GetComponentInChildren<HopeAnimsController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!MasterStaticScript.gameIsPaused)
        {
            //start state machine
            line = new Ray(cameraPivot.transform.position, cameraPivot.transform.forward);
            switch (playerState)
            {                
                    //ready to fire (normal)
                case 1:
                    TestLine();
                    if (Input.GetMouseButtonDown(0) && canHit)
                    {
                        //fire hook

                        if (FireHook())
                        {                            
                            playerState = 2;
                        }
                    }
                    break;
                    //being pulled
                case 2:
                    ReelIn();
                    MakeLines();
                    ropeDis = Vector3.Distance(player.transform.position, hook.transform.position);

                    if (ropeDis < 4)
                    {
                        //if grounded, reset
                        if (IsGrounded.downHook)
                        {
                            playerState = 1;
                            Reset();
                            return;
                        }// else print("not grounded");

                        //if hitting a cieling, reset
                        if (IsGrounded.up)
                        {
                            playerState = 1;
                            Reset();
                            return;
                        }//else print("not bonking");
                        //if hitting a wall, reset
                        if (CanClimb.cannotClimb)
                        {
                            playerState = 1;
                            Reset();
                            return;
                        }
                        //else print("can climb");
                        //then you must be good to climb

                            DestroyLines();
                            targetClimbPosition = climbDetector.transform.position + climbDetector.transform.forward *forwardOffset+ climbDetector.transform.up *verticalOffset;
                            startClimbPosition = player.transform.position;
                            if(useHelperCube) helperCube.transform.position = targetClimbPosition;
                            playerState = 3;
                            climbTimer = 0;
                        
                        //climb check

                        //else return to normal
                    }
                    break;

                case 3:
                    //climbing
                    //player.transform.Translate(Vector3.up * upAmount);
                    //player.transform.Translate(climbDetector.transform.forward * forwardAmount);
                    //player.transform.position = Vector3.MoveTowards(player.transform.position, targetClimbPosition, climbSpeed);

                    player.transform.position = Vector3.Slerp(startClimbPosition, targetClimbPosition,  climbTimer / howLongToClimbFor);

                    if (IsGrounded.down || climbTimer > howLongToClimbFor || IsGrounded.up)
                    {
                        playerState = 1;
                        Reset();
                    }
                    climbTimer += Time.deltaTime;
                    break;
            }  

            if (player.GetComponent<ControlSwap>().controlState != 0)
            {
                //switch to default
                Reset();
            }
        }
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
        layerMask = 1 << 2;
        layerMask = ~layerMask;
    }

    private void Reset()
    {
        playerState = 1;
        player.GetComponent<CharacterController>().enabled = true;
        hook.transform.position = cameraPivot.transform.position;
        DestroyLines();
        BaseMovementModule.gravity = -35;
        hook.transform.position = cameraPivot.transform.position;
        hopeAnims.hooked = false;
    }
    private void DestroyLines()
    {
        //rope.SetVertexCount(0);
        rope.positionCount = 0;
    }
    private bool FireHook()
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

               

                //switch to being pulled

                return true;
            }
        }
        return false;
    }
    private void MakeLines()
    {
        //rope.SetVertexCount(2);
        
        rope.positionCount = 2;
        rope.SetPosition(0, hookHolder.transform.position);
        rope.SetPosition(1, hook.transform.position);
    }
    private void ReelIn()
    {
        hopeAnims.hooked = true;
        ropeDis = Vector3.Distance(player.transform.position, hook.transform.position);
        
        playerController.enabled = false;
        BaseMovementModule.gravity = 0;
        playerBody.velocity = Vector3.zero;
        playerMove.direction = Vector3.zero;
        player.transform.position = Vector3.MoveTowards(player.transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);


    }
}
