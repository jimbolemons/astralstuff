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
    }

    public override void TriggerOnDeath()
    {
        Destroy(gameObject);
    }
}
