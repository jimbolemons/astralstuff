using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PickupType
{
    power, speed, health
}


public class PickUp : MonoBehaviour
{
    GameObject player;
    PlayerHP hp;
   // PlayerController player;
    public PickupType type;
    //    public Placement placement;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player 1");
        //player = player.GetComponent<PlayerHP>();
        hp = player.GetComponent<PlayerHP>();

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log(type);
            hp.CollectedPickUp(type);
            Destroy(this.gameObject);
        }
    }
}
