using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCharge : Gun
{
    public bool charging = false;
    public float power = 1;
    public float basePower = 1;
    public float maxPower = 5;
    public float chargeSpeed = 2;

    public HadokenControl control;

    void Update()
    {
        if (charging)
        {
            if(power < maxPower)
            {
                power += Time.deltaTime * chargeSpeed;
            }
            control.SetPower(power);
        }
    }
    public override void Fire()
    {
        if (canFire)
        {
            power = basePower;

            StartCharge();
            canFire = false;
            Invoke("ResetFire", fireCooldown);
        }
    }
    void StartCharge()
    {
        charging = true;
        
        GameObject g = Instantiate(projectilePrefab, gunEnd.position, gunEnd.rotation);
        
        control = g.GetComponentInChildren<HadokenControl>();
        control.SetParent(gameObject);
       
        try
        {
            g.GetComponentInChildren<BulletStats>().SetParent(parentType);
            
        }
        catch
        {
            g.GetComponent<ObjectWithHealth>().SetParent(parentType);
        }
    }

    public override void StopFire()
    {
        control.TimeToGo();
        //reset to defaults
        charging = false;
        power = basePower;
        control = null;
    }
}
