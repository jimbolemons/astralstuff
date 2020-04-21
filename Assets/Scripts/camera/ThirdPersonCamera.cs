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
    [Tooltip("Player's cameratarget object.")]
    public Transform cameraTarget;
    //public Transform pivot;
    public GameObject pivot;
    public GameObject mainCamera;

    [Tooltip("Using mouse controls?")]
    public bool mouseControl = true;
    public float mouseSensitivity = 10;

    //how far back the camera sits
    float distanceFromTarget;
    public Vector2 yMinMax = new Vector2(-40, 40);

    //a bit of dampening on the rotation
    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    public float cameraSnapDistance = 0.5f;

    Ray line;
    int layerMask = 0;
    bool raycastConfirm = false;
    RaycastHit hit;

    public float cameraOffsetAngle = 15;
    public float cameraDis = -20;
    public float cameraMoveSpeed;
    public float maxLerpTime;
    float lerpTimer;

    public float minCamDis;

    public bool lerping = true;

    Vector3 wantedPos;

    public CameraShake cameraShake;

    private void Start()
    {
        //cameraDis = cameraDisDefult;
        //wantedPos = pivot.transform.position;        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //only run if game isn't paused
        if (!MasterStaticScript.gameIsPaused)
        {
            //set position to be camera target, so we follow player
            //transform.position = cameraTarget.transform.position;

            float distance = Vector3.Distance(transform.position, cameraTarget.transform.position);
            if (distance < minCamDis) lerping = false;

            if (lerping)
            {
                lerpTimer += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, cameraTarget.transform.position, lerpTimer/maxLerpTime);
            }
            else
            {
                lerpTimer = 0;
                transform.position = cameraTarget.transform.position;
            }
            if(raycastConfirm)
            {
                mainCamera.transform.position = wantedPos;
            }
            else
            {
               UpdateCameraDistance();
            }
           // mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, wantedPos + transform.forward * 2, Time.deltaTime * cameraMoveSpeed);

            //if camera distance has been updated, fix camera position
           // UpdateCameraDistance();

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


            cameraTarget.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }


    public void SetLerp(bool x)
    {        
        lerping = x;        
    }
    void UpdateCameraDistance()
    {
        float x = mainCamera.transform.localRotation.x;
        if (x != -cameraOffsetAngle)
        {
            mainCamera.transform.localRotation = Quaternion.Euler(-cameraOffsetAngle, 0, 0);
        }
        //mainCamera.transform.eulerAngles = new Vector3(cameraOffsetAngle, 0, 0);
        float z = mainCamera.transform.localPosition.z;
        if (-z != cameraDis)
        {
            mainCamera.transform.localPosition = new Vector3(0, 0, -cameraDis);
           // pivot.transform.position = new Vector3(0, -1.5f, cameraDis);
        }
    }

    void CheckRayCamera()
    {
        //checks only on ground layer
        layerMask = 1 << 12;
        //layerMask = ~layerMask;
        line = new Ray(transform.position, -transform.forward);
        wantedPos = transform.position - transform.forward * cameraDis;        

        if (Physics.Raycast(line, out hit, cameraDis, layerMask))
        {
            wantedPos = hit.point;
            raycastConfirm = true;
        }
        else
        {
            raycastConfirm = false;
            Debug.DrawRay(pivot.transform.position, -pivot.transform.forward * cameraDis, Color.green);
        }

    }
}
