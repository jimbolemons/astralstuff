using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Gun Class
/// Firing script that causes the weapon to fire after a short delay
/// </summary>
public class ShootWithDelay : Gun
{
    [Tooltip("How long to delay the shot.")]
    public float delayToShoot = 0.1f;
    public override void Fire()
    {
        if (canFire)
        {
            Invoke("DelayedFire", delayToShoot);
            canFire = false;
            Invoke("ResetFire", fireCooldown);
        }
    }
    /// <summary>
    /// Fire with a delay
    /// </summary>
    void DelayedFire()
    {
        GameObject g = Instantiate(projectilePrefab, gunEnd.position, gunEnd.rotation);
        g.GetComponent<BulletStats>().SetParent(parentType);
    }
}
