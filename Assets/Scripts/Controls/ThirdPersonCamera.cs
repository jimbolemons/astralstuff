using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Controls for the 3rd person camera that orbits the player model
/// </summary>
public class ThirdPersonCamera : MonoBehaviour
{
    float y;
    float x;
    [Tooltip("Player's camera target object.")]
    public Transform target;
    //public Transform pivot;
    public GameObject pivot;
    public GameObject cameras;
    
    [Tooltip("Using mouse controls?")]
    public  bool mouseControl = true;
    public float mouseSensitivity = 10;

    //how far back the camera sits
    float distanceFromTarget;
    public Vector2 yMinMax = new Vector2(-40, 40);

    //a bit of dampening on the rotation
    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    Ray line;
    int layerMask = 0;
    RaycastHit hit;
    public float cameraDisDefult;
    float cameraDis;
    public float cameraMoveSpeed;
    public float minCamDis;
    Vector3 wantedPos;

    public CameraShake cameraShake;

    private void Start()
    {
        cameraDis = cameraDisDefult;
        wantedPos = pivot.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        //pivot.transform.position = target.transform.position;
        //only run if game isn't paused
        if (!MasterStaticScript.gameIsPaused)
        {
            CheckRayCamera();
            if (mouseControl)
            {
                x += Input.GetAxis("Mouse X") * mouseSensitivity;
                y -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            }
            
            //clamps up and down camera movment
            y = Mathf.Clamp(y, yMinMax.x, yMinMax.y);


            
            //rotation stuff
            Vector3 targetRotation = new Vector3(y, x);
            currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref rotationSmoothVelocity, rotationSmoothTime);
            transform.eulerAngles = currentRotation;
            // StartCoroutine(cameraShake.Shake(.15f, .4f));


            //moves the camera twards where it wants to be            
           
            transform.position = Vector3.Lerp(cameras.transform.position, wantedPos + transform.forward, Time.deltaTime * cameraMoveSpeed);

            //keeps the pivot in place
            pivot.transform.position = target.transform.position;                                

            //rotation stuff
            target.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

            /// testing random shit down here
            // cameras.transform.position =  ;
            // cameras.transform.position = target.position - transform.forward * cameraDis;
            // cameraDis = Mathf.Lerp(cameras.transform.position.z, wantedPos.z, Time.deltaTime * cameraMoveSpeed);

        }
    }
    void CheckRayCamera()
    {
        //checks only on ground layer
        layerMask = 1 << 12;
        //layerMask = ~layerMask;
        line = new Ray(pivot.transform.position, -pivot.transform.forward);
        wantedPos = target.position - transform.forward * cameraDisDefult;

        if (Physics.Raycast(line, out hit, cameraDis, layerMask))
        {
            wantedPos = hit.point;
        }
        else
       
        Debug.DrawRay(pivot.transform.position, -pivot.transform.forward * cameraDis, Color.green);
    }

    
}
