using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ObjectWithHealth Class
/// Meant for terrain to enable it to stop bullets
/// </summary>
public class TerrainHP : ObjectWithHealth
{
    private void Start()
    {
        objectType = objectWithHealthType.terrain;
    }

    public override void TriggerOnDeath()
    {

    }
    public override void TriggerOnDamage()
    {
        throw new System.NotImplementedException();
    }
}
