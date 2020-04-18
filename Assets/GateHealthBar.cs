using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateHealthBar : MonoBehaviour
{   
    public Slider healthBarSlider;

    public float healthBarTimeout = 15;
    //default this to a very low value so the health bar turns off
    public float healthBarTimeoutTimer = .01f;

    public GameObject parent;

    float pHealthValue;
    public ObjectWithHealth gateHealth;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        healthBarTimeoutTimer -= Time.deltaTime;

        if(healthBarTimeoutTimer <= 0)
        {
            parent.SetActive(false);
          //  print("turn off parent");
        }

        //Debug.Log("health bar size: " + gateHealth.health / gateHealth.maxHealth);
        //transform.localScale = new Vector3(gateHealth.health / gateHealth.maxHealth, 1, 1);
        //healthBarSlider.value = gateHealth.health;
    }

    public void UpdateValues(float currentHealth, float maxHealth)
    {
       // gateHealth = newGateHealth;
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
        healthBarTimeoutTimer = healthBarTimeout;
    }
}
