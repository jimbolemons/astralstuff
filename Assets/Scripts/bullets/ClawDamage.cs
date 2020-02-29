using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawDamage : BulletDamage
{
    override public void OnTriggerEnter(Collider other)
    {
        ObjectWithHealth target = other.gameObject.GetComponent<ObjectWithHealth>();

        if (target != null)
        {

            //only interact with whatever you hit if it's not the same type as the gameobject that fired the bullet
            if (parentType != target.objectType)
            {
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
                    if (target.tag != "SacredSite")
                    {
                        target.TakeDamage(damage);
                        //StartCoroutine(cameraShake.Shake(.15f, .4f));

                        //Destroy(gameObject);
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
