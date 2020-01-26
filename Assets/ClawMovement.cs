using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMovement : MonoBehaviour
{
    [Tooltip("How fast the attack rotates.")]
    public float speed = 2;
    [Tooltip("How long Does the hitbox linger")]
    public float lifeSpan = 1;

    public float angleToRotate = 45;

    Quaternion targetRotation;
    void Start()
    {
        targetRotation = transform.rotation;
        targetRotation *= Quaternion.AngleAxis(angleToRotate, Vector3.up);
    }


    void Update()
    {
        //if game is not paused
        if (MasterStaticScript.gameIsPaused == false)
        {
            //move forward
            //transform.position += this.transform.forward * speed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation,  speed * Time.deltaTime);
            lifeSpan -= Time.deltaTime;
            if (lifeSpan <= 0)
            {
                DestroySelf();
            }
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}