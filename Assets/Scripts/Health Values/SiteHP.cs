using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteHP : ObjectWithHealth
{
    public bool isDead = false;
    private void Start()
    {
        objectType = objectWithHealthType.destructible;
        MasterStaticScript.sacredSites.Add(gameObject);
    }

    public void Update()
    {
        if (isDead) killSite();
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

    private void killSite()
    {
        print("Site is dead.");
        MasterStaticScript.RemoveFromObjectList(gameObject, MasterStaticScript.sacredSites);
        MasterStaticScript.CheckForGameLose();
        Destroy(gameObject);
    }
    public override void TriggerOnDamage()
    {
        throw new System.NotImplementedException();
    }
}
