using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ObjectWithHealth Class
/// Target dummy / barrel health value
/// </summary>
public class BarrelHP : ObjectWithHealth
{
    private void Start()
    {
        objectType = objectWithHealthType.destructible;
    }


    public override void TriggerOnDeath()
    {
        //Explosions totally go here!
        //print("Barrel go boom");
        Destroy(gameObject);
    }
}
