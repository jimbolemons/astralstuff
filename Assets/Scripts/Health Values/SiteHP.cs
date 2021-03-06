﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteHP : ObjectWithHealth
{
    public bool isDead = false;
    public GameObject expansionRing;
    public float playerWarningDistance = 80;
    public AudioManager audio;
    public AudioSource sitehit;

    private void Start()
    {
        objectType = objectWithHealthType.destructible;
        MasterStaticScript.sacredSites.Add(gameObject);
        audio = FindObjectOfType<AudioManager>();
    }

    public void Update()
    {
        if (isDead) killSite();
        else
        {
            //do your thing.
        }
       // Debug.Log(MasterStaticScript.playerReference.transform.position);
    }


    public override void TriggerOnDeath()
    {
        //Explosions totally go here!
        //print("Barrel go boom");
        audio.Play("sitedeath");
        isDead = true;

    }

    private void killSite()
    {
        //print("Site is dead.");
        MasterStaticScript.RemoveFromObjectList(gameObject, MasterStaticScript.sacredSites);

        MasterStaticScript.CheckForGameLose();
        Destroy(gameObject);
    }
    public override void TriggerOnDamage()
    {
       sitehit.Play();
        //SOUND
        //Debug.Log("site has taken Damage!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        float dist = Vector3.Distance(transform.position, MasterStaticScript.playerReference.transform.position);
        if (dist > playerWarningDistance) {
        Instantiate(expansionRing, new Vector3(transform.position.x, transform.position.y + 40, transform.position.z), Quaternion.identity);
        }
    }
}
