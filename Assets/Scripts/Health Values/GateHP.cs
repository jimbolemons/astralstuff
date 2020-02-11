using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateHP : ObjectWithHealth
{
    public bool isDead = false;
    private void Start()
    {
        objectType = objectWithHealthType.destructible;
        MasterStaticScript.enemyGates.Add(gameObject);
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
        throw new System.NotImplementedException();
    }
}
