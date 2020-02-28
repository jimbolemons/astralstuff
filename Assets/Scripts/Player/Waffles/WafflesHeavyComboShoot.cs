using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WafflesHeavyComboShoot : Gun 
{ 

    public float minTimeBetweenShots = .2f;
    public float maxTimeBetweenShots = .5f;
    public float comboTimer = 0;
    public int comboCounter = 0;
    public int maxCombo = 3;

    public bool failCombo = false;

    public Transform gunEnd2;

    // Start is called before the first frame update
    void Start()
    {
        if (minTimeBetweenShots > maxTimeBetweenShots)
        {
            throw new System.Exception("ERROR: waffles combo min time is greater than max time. THE ATTACK CANNOT HAPPEN!!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (comboCounter > 0)
        {
            comboTimer += Time.deltaTime;
        }
        if (comboTimer > maxTimeBetweenShots)
        {
            comboCounter = 0;
            comboTimer = 0;
        }
    }

    public override void Fire()
    {
        if (canFire)
        {
            if (comboCounter == 0)
            {
                Fire2();
                comboTimer = 0;
                comboCounter++;
            }
            else if (comboCounter == 1)
            {
                if (ComboCheck())
                {
                    Fire1();
                    comboTimer = 0;
                    comboCounter++;
                }                
            }
            else if (comboCounter == 2)
            {
                if (ComboCheck())
                {
                    Fire1();
                    Fire2();
                    comboTimer = 0;
                    comboCounter++;
                }                
            }
            else
            {
                ResetCombo();
            }
            if (failCombo || comboCounter >= maxCombo)
            {
                ResetCombo();
            }
            // print(comboCounter);
            // print(ComboCheck());          
        }
    }
    void ResetCombo()
    {
        // print("noob, resetting");
        Invoke("ResetFire", fireCooldown);
        comboCounter = 0;
        comboTimer = 0;
        failCombo = false;
        canFire = false;
    }

    bool ComboCheck()
    {
        if (comboCounter < minTimeBetweenShots) return false;
        if (comboCounter > maxTimeBetweenShots) return false;
        return true;
    }
    void Fire1()
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
    }
    void Fire2()
    {
        GameObject g = Instantiate(projectilePrefab, gunEnd2.position, gunEnd2.rotation);
        g.transform.SetParent(this.transform);
        try
        {
            g.GetComponent<BulletStats>().SetParent(parentType);
        }
        catch
        {
            g.GetComponent<ObjectWithHealth>().SetParent(parentType);
        }
    }
}
