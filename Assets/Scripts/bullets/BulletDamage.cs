﻿using System.Collections;
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
    public CameraShake cameraShake;
    public GameObject cameras;
    public ParticleSystem emit;
    private void Start()
    
    {
        GameObject player = GameObject.Find("Player 1");
        if (playerBullet)
        {
            if (player.GetComponent<PickupsManager>().usingPower)
            {
                MoreDam();
            }
            //Debug.Log(damage);
        }
        cameras = GameObject.Find("/cameraHolder/Camera");
        cameraShake = cameras.GetComponent<CameraShake>();

    }
    private void MoreDam()
    {
        damage = damage * 2;
    }
    public virtual void OnTriggerEnter(Collider other)
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

                        Destroy(gameObject);
                    }
                }
                else
                if (parentType == ObjectWithHealth.objectWithHealthType.player)
                {
                    if (target.tag != "SacredSite")
                   {
                        target.TakeDamage(damage);
                        //StartCoroutine(cameraShake.Shake(.15f, .4f));

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
    public virtual void OnCollisionEnter(Collision collision)
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
                        if(target.GetComponent<ObjectWithHealth>().objectType == ObjectWithHealth.objectWithHealthType.player)
                        {
                            StartCoroutine(cameraShake.Shake(.15f, .4f));
                        }
                        
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
    public void DestroySelf()
    {
        if(emit!= null) DetachParticles();
        Destroy(gameObject);
    }
    void DetachParticles()
    {
        // This splits the particle off so it doesn't get deleted with the parent
        emit.transform.parent = null;

        // this stops the particle from creating more bits
        emit.Stop();
    }
}
