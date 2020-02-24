﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ObjectWithHealth Class
/// Default enemy health stats
/// </summary>
public class EnemyHP : ObjectWithHealth
{
    public CameraShake cameraShake;
    public GameObject cameras;
    private void Start()
    {
        objectType = objectWithHealthType.enemy;
        MasterStaticScript.enemyList.Add(gameObject);
        //print("Enemies currently in Array: " + MasterStaticScript.enemyList.Count);
        cameras = GameObject.Find("/cameraHolder/Camera");
        cameraShake = cameras.GetComponent<CameraShake>();
    }
   


    public override void TriggerOnDeath()
    {
        StartCoroutine(cameraShake.Shake(.15f, .4f));
        MasterStaticScript.enemyList.Remove(gameObject);
        Destroy(gameObject);
        
    }
    public override void TriggerOnDamage()
    {
        StartCoroutine(cameraShake.Shake(.15f, .4f));
    }
}
