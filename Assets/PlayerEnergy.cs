using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{

    public float maxEnergy = 100;
    public float energy = 100;

    public float initialCost = 5;
    public float costPerSec = 5;

    public float meleeEnergyBonus = 20;

    public float regenPerSec = 2;

    bool charging = false;
    // Start is called before the first frame update
    void Start()
    {
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        if (!charging)
        {
            energy += regenPerSec * Time.deltaTime;
            OverFlowCheck();
        }
        if (charging)
        {
            energy -= costPerSec * Time.deltaTime;
            if (energy <= 0) StopCharging();
        }
    }

    //attempt to start charging, if you have enough energy to charge, start charging
    //if not enough energy, return false;
    public bool StartCharging()
    {
        if(energy < initialCost)
        {
            return false;
        }
        energy -= initialCost;
        charging = true;

        return true;
    }

    public bool IsCharging()
    {
        return charging;
    }
    public void StopCharging()
    {
        charging = false;
    }

    public void MeleeHit()
    {
        AddEnergy(meleeEnergyBonus);
    }
   void AddEnergy(float amountToAdd)
    {
        energy += amountToAdd;
        OverFlowCheck();
    }
    private void OverFlowCheck()
    {
        if (energy > maxEnergy)
        {
            energy = maxEnergy;
        }
    }
}
