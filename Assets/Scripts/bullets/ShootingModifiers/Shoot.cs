﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gun class
/// The most basic shooting script, simply fires bullets
/// </summary>
public class Shoot : Gun
{
    public override void Fire()
    {
        if (canFire)
        {
            GameObject g = Instantiate(projectilePrefab, gunEnd.position, gunEnd.rotation);

            try
            {
                g.GetComponent<BulletStats>().SetParent(parentType);
            }
            catch
            {
                g.GetComponent<ObjectWithHealth>().SetParent(parentType);
            }
            canFire = false;
            Invoke("ResetFire", fireCooldown);
        }
    }
}
