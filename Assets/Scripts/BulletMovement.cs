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


    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
