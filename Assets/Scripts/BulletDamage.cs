using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// BulletStats Class
/// This script handles bullet collisions
/// </summary>
public class BulletDamage : BulletStats
{
    /// <summary>
    /// if the bullet collides with a trigger volume
    /// </summary>
    /// <param name="other"></param>

    public bool playerBullet;
    private void Start()
    {
        GameObject player = GameObject.Find("Player 1");
        if (playerBullet)
        {
            if (player.GetComponent<PickupsManager>().usingPower)
            {
                MoreDam();
            }
            Debug.Log(damage);
        }

    }
    private void MoreDam()
    {
        damage = damage * 2;
    }
    private void OnTriggerEnter(Collider other)
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
                        Destroy(gameObject);
                    }
                }
                else
                if (parentType == ObjectWithHealth.objectWithHealthType.player)
                {
                    if (target.tag != "SacredSite")
                    {
                        target.TakeDamage(damage);
                        Destroy(gameObject);
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
    private void OnCollisionEnter(Collision collision)
    {
        ObjectWithHealth target = collision.gameObject.GetComponent<ObjectWithHealth>();

        if (target != null)
        {
            //only interact with whatever you hit if it's not the same type as the gameobject that fired the bullet
            if (parentType != target.objectType)
            {
                if (parentType == ObjectWithHealth.objectWithHealthType.enemy){
                    if(target.tag!= "EnemyGate")
                    {
                        target.TakeDamage(damage);
                        Destroy(gameObject);
                    }
                }else 
                if (parentType == ObjectWithHealth.objectWithHealthType.player)
                {
                    if(target.tag!= "SacredSite")
                    {
                        target.TakeDamage(damage);
                        Destroy(gameObject);
                    }
                }
            }
            //print("I HIT A THING! " + collision.gameObject.name);
        }
    }
}
