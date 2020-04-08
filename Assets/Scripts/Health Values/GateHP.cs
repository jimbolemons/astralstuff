using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateHP : ObjectWithHealth
{
    public bool isDead = false;
    public CameraShake cameraShake;
    public GameObject camera;

    public bool doCameraShake;

    float prevPercent;
    private void Start()
    {
        objectType = objectWithHealthType.destructible;
        MasterStaticScript.enemyGates.Add(gameObject);
    }

    public void Update()
    {
        prevPercent = health / maxHealth;
        if (isDead) killGate();
        else
        {
            //do your thing.
        }
    }


    public override void TriggerOnDeath()
    {
        //Explosions totally go here!
        //print("Barrel go boom");
        isDead = true;

    }

    private void killGate()
    {
        print("Gate is dead.");
        MasterStaticScript.RemoveFromObjectList(gameObject, MasterStaticScript.enemyGates);
        MasterStaticScript.CheckForGameWin();
        Destroy(gameObject);
    }
    public override void TriggerOnDamage()
    {
        //logic for making camera shake (or other thing) when 1/3 breakpoints hit
        if (doCameraShake)
        {
            if (prevPercent >= .66 && health / maxHealth <= .66)
            {
                StartCoroutine(cameraShake.Shake(.2f, .4f));
            }
            else
            if (prevPercent >= .33 && health / maxHealth <= .33)
            {
                StartCoroutine(cameraShake.Shake(.5f, .6f));
            }
        }
        FindObjectOfType<AudioManager>().Play("timeScream");
        //SOUND
    }
}
