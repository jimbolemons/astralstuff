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
    public Transform pivot;
    
    [Tooltip("Using mouse controls?")]
    public  bool mouseControl = true;
    public float mouseSensitivity = 10;

    //how far back the camera sits
    public float distanceFromTarget = 4;
    public Vector2 yMinMax = new Vector2(-40, 85);

    //a bit of dampening on the rotation
    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    // Update is called once per frame
    void LateUpdate()
    {
        //only run if game isn't paused
        if (!MasterStaticScript.gameIsPaused)
        {
            if (mouseControl)
            {
                x += Input.GetAxis("Mouse X") * mouseSensitivity;
                y -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            }
            pivot.transform.position = target.transform.position;

            y = Mathf.Clamp(y, yMinMax.x, yMinMax.y);

            Vector3 targetRotation = new Vector3(y, x);
            currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref rotationSmoothVelocity, rotationSmoothTime);

            transform.eulerAngles = currentRotation;

            transform.position = target.position - transform.forward * distanceFromTarget;

            pivot.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }
}
