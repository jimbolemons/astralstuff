using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateHealthBar : MonoBehaviour
{

    public Slider healthBarSlider;
    public ObjectWithHealth gateHealth;
    void Start()
    {
        //TODO : fix this to get reference from master static script
        // player = MasterStaticScript.playerReference;

        healthBarSlider.maxValue = gateHealth.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("health bar size: " + gateHealth.health / gateHealth.maxHealth);
        //transform.localScale = new Vector3(gateHealth.health / gateHealth.maxHealth, 1, 1);
        healthBarSlider.value = gateHealth.health;
    }
}
