using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ObjectWithHealth Class
/// Default enemy health stats
/// </summary>
public class EnemyHP : ObjectWithHealth
{
    private void Start()
    {
        objectType = objectWithHealthType.enemy;
        MasterStaticScript.enemyList.Add(gameObject);
        print("Enemies currently in Array: " + MasterStaticScript.enemyList.Count);
    }

    private void Update()
    {
        foreach(GameObject site in MasterStaticScript.sacredSites)
        {
            //check collision between enemy and site:
            
                //if collision has happened, mark current site as dead.

                //else, do nothing.
        }
    }

    public override void TriggerOnDeath()
    {
        MasterStaticScript.enemyList.Remove(gameObject);
        Destroy(gameObject);
    }
}
