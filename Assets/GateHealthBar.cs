using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateHealthBar : MonoBehaviour
{

    
    public ObjectWithHealth gateHealth;
    void Start()
    {
        //TODO : fix this to get reference from master static script
        // player = MasterStaticScript.playerReference;
    
    
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("health bar size: " + gateHealth.health / gateHealth.maxHealth);
        transform.localScale = new Vector3(gateHealth.health / gateHealth.maxHealth, 1, 1);
    }
}
