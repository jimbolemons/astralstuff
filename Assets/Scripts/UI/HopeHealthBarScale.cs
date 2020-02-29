using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopeHealthBarScale : MonoBehaviour
{

    public GameObject player;
    public ObjectWithHealth playerHealth;
    void Start()
    {
        //TODO : fix this to get reference from master static script
       // player = MasterStaticScript.playerReference;
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerHealth = player.GetComponent<ObjectWithHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(playerHealth.health / playerHealth.maxHealth,1, 1);
    }
}
