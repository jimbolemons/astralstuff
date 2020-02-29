using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WafflesEnergyBarScale : MonoBehaviour
{

    public GameObject player;
    public PlayerEnergy playerEnergy;
    void Start()
    {
        //TODO : fix this to get reference from master static script
        // player = MasterStaticScript.playerReference;
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerEnergy = player.GetComponent<PlayerEnergy>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(playerEnergy.energy / playerEnergy.maxEnergy, 1, 1);
    }
}
