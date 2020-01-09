using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Gun class
/// Fires the bullet as a child of the parent
/// This means it will follow the parent's movement over it's lifetime
/// </summary>
public class ShootAsChild : Gun
{
    public override void Fire()
    {
        if (canFire)
        {
            GameObject g = Instantiate(projectilePrefab, gunEnd.position, gunEnd.rotation);

            g.transform.SetParent(this.transform);

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
