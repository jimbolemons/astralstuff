using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateHP : ObjectWithHealth
{
    public bool isDead = false;
    public CameraShake cameraShake;
    public GameObject camera;

    public GameObject UIHealthBar;
    GateHealthBar UIHealthBarScript;

    SpawnDemons demonController;

    public bool doCameraShake;

    float prevPercent;
    private void Start()
    {
        demonController = GetComponent<SpawnDemons>();
        if(UIHealthBar == null)
        {
            //print("searching");
            UIHealthBar = GameObject.Find("GateHealthBar").GetComponent<GameObject>();
        }
        UIHealthBarScript = UIHealthBar.GetComponentInChildren<GateHealthBar>();

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
        FindObjectOfType<AudioManager>().Play("gatedeath");
        isDead = true;

    }

    private void killGate()
    {
        demonController.SendOutTheDemons();
        print("Gate is dead.");
        UIHealthBar.SetActive(false);
        MasterStaticScript.RemoveFromObjectList(gameObject, MasterStaticScript.enemyGates);
        MasterStaticScript.CheckForGameWin();
        Destroy(gameObject);
    }
    public override void TriggerOnDamage()
    {
     
        UIHealthBar.SetActive(true);
        UIHealthBarScript.UpdateValues(health, maxHealth);

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
        FindObjectOfType<AudioManager>().Play("gatehit");
        //SOUND
    }
}
