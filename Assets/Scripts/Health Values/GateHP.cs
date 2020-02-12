using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateHP : ObjectWithHealth
{
    public bool isDead = false;
    public CameraShake cameraShake;
    public GameObject camera;
    private void Start()
    {
        objectType = objectWithHealthType.destructible;
        MasterStaticScript.enemyGates.Add(gameObject);
        camera = GameObject.Find("/cameraHolder/Camera");
        cameraShake = camera.GetComponent<CameraShake>();
    }

    public void Update()
    {
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
        StartCoroutine(cameraShake.Shake(.15f, .4f));
    }
}
