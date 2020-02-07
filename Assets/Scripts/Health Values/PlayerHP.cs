using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ObjectWithHealth Class
/// Player's health value
/// </summary>
public class PlayerHP : ObjectWithHealth
{
    public PickupsManager pickups;
    public bool isDead = false;
    private void Start()
    {
        objectType = objectWithHealthType.player;
    }

    public void Update()
    {
        if (isDead) killPlayer();
        else
        {
            //do your thing.
        }
    }

    public override void TriggerOnDeath()
    {        
        isDead = true;        
    }

    private void killPlayer()
    {
        print("player down!! Player down!!");        
        MasterStaticScript.PlayerDead();
       // Destroy(gameObject);
    }
    public void CollectedPickUp(PickupType type)
    {
        pickups.CollectedPickUp(type);
    }
}
