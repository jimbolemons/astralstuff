using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class simply makes a bullet move forward
/// </summary>
public class BulletMovement : MonoBehaviour
{
    [Tooltip("How fast the bullet moves.")]
    public float speed = 20;
    [Tooltip("How long til the bullet times out.")]
    public float lifeSpan = 4;
     public ParticleSystem emit;
    public bool moving = true;
    void Start()
    {
        //Invoke("DestroySelf", 4);
    }


    void Update()
    {
        //if game is not paused
        if (MasterStaticScript.gameIsPaused == false)
        {
            //move forward
            if (moving)
            {

                transform.position += this.transform.forward * speed * Time.deltaTime;
                lifeSpan -= Time.deltaTime;
                if (lifeSpan <= 0)
                {
                    DestroySelf();
                }
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
