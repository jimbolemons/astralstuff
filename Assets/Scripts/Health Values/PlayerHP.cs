using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ObjectWithHealth Class
/// Player's health value
/// </summary>
public class PlayerHP : ObjectWithHealth
{
    private void Start()
    {
        objectType = objectWithHealthType.player;
    }

    public override void TriggerOnDeath()
    {

        Debug.Log("player down!! Player down!!");
    }
}
