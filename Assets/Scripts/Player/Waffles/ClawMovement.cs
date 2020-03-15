using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClawMovement : MonoBehaviour
{
    [Tooltip("How fast the attack rotates.")]
    public float speed = 4;
    [Tooltip("How long Does the hitbox linger")]
    public float lifeSpan = .5f;
    float speed2 = 2;
    bool grow = true;

    public float angleToRotate = 45;
    public ParticleSystem emit;

    

    Vector3 originalScale;
    Vector3 destinationScale;

    Quaternion targetRotation;
    void Start()
    {
        originalScale = transform.localScale;
        transform.localScale = Vector3.zero;
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
    void DetachParticles()
    {
        // This splits the particle off so it doesn't get deleted with the parent
        emit.transform.parent = null;

        // this stops the particle from creating more bits
        emit.Stop();
    }
    public void DestroySelf()
    {
        DetachParticles();
        Destroy(gameObject);
    }


   
}