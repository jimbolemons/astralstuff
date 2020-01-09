using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ObjectWithHealth Class
/// The 'shield gun' health value so it can be broken
/// </summary>
public class ShieldHP : ObjectWithHealth
{

    public override void TriggerOnDeath()
    {
        Destroy(gameObject);
    }
}
