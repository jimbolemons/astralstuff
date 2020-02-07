using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMovement : MonoBehaviour
{
    [Tooltip("How fast the attack rotates.")]
    public float speed = 2;
    [Tooltip("How long Does the hitbox linger")]
    public float lifeSpan = 1;
    float speed2 = .5f;
    bool grow = true;

    public float angleToRotate = 45;

    Vector3 originalScale;
    Vector3 destinationScale;

    Quaternion targetRotation;
    void Start()
    {
        transform.localScale = Vector3.zero;
        originalScale = transform.localScale;
        destinationScale = new Vector3(2.0f, 2.0f, 2.0f);
        
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
            transform.localScale = Vector3.Lerp(originalScale, destinationScale, speed2);
            speed2 += Time.deltaTime;
            //transform.localScale = new Vector3(10,10,10);

            /*
            if (speed2 >= 1)
            {
                grow = false;
            }
            else if(speed2 <= 0)
            {
                grow = true;
            }
            
            if (grow)
            {
                
            }
            else
            {
                transform.localScale = Vector3.Lerp(destinationScale, originalScale, speed2);
                speed2 -= Time.deltaTime;
            }
            */

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