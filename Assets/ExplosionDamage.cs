using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : BulletDamage
{
  
    override public void OnTriggerEnter(Collider other)
    {
        //print("HIT SOMETHING??!?!? collider");
        ObjectWithHealth target = other.gameObject.GetComponent<ObjectWithHealth>();

        if (target != null)
        {
            //only interact with whatever you hit if it's not the same type as the gameobject that fired the bullet
            if (parentType != target.objectType)
            {
                //enemies cannot hit gates
                if (parentType == ObjectWithHealth.objectWithHealthType.enemy)
                {
                    if (target.tag != "EnemyGate")
                    {
                        target.TakeDamage(damage);
                        //StartCoroutine(cameraShake.Shake(.15f, .4f));

                        //Destroy(gameObject);
                    }
                }
                else
                if (parentType == ObjectWithHealth.objectWithHealthType.player)
                {
                    // print("blood TEEEST??");
                    //player cannot hit sacred sites
                    if (target.tag != "SacredSite")
                    {
                        target.TakeDamage(damage);
                        //StartCoroutine(cameraShake.Shake(.15f, .4f));

                        //Destroy(gameObject);
                    }
                    if (target.objectType == ObjectWithHealth.objectWithHealthType.enemy)
                    {
                        // Debug.Log("spawning blood 2");             
                        //Instantiate(bloodSplat, target.transform.position, Quaternion.Euler(other.coll collision.contacts[0].normal));

                    }
                }
            }
            //print("I HIT A THING! " + other.gameObject.name);
        }
    }
    /// <summary>
    /// when the bullet collides with another collider
    /// </summary>
    /// <param name="collision"></param>
    override public void OnCollisionEnter(Collision collision)
    {
        ObjectWithHealth target = collision.gameObject.GetComponent<ObjectWithHealth>();
        if (target != null)
        {
            //only interact with whatever you hit if it's not the same type as the gameobject that fired the bullet
            if (parentType != target.objectType)
            {
                if (parentType == ObjectWithHealth.objectWithHealthType.enemy)
                {
                    if (target.tag != "EnemyGate")
                    {
                        if (target.GetComponent<ObjectWithHealth>().objectType == ObjectWithHealth.objectWithHealthType.player)
                        {
                            StartCoroutine(cameraShake.Shake(.15f, .4f));
                        }

                        target.TakeDamage(damage);
                        // Destroy(gameObject);
                    }
                }
                else
                if (parentType == ObjectWithHealth.objectWithHealthType.player)
                {

                    //player cannot hit sacred site
                    if (target.tag != "SacredSite")
                    {
                        target.TakeDamage(damage);
                        //Destroy(gameObject);
                    }
                }
            }
            //print("I HIT A THING! " + collision.gameObject.name);
        }
    }
}
