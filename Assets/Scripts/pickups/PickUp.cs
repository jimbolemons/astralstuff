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
    void Start()
    {
        //player = MasterStaticScript.playerReference;
        //player = player.GetComponent<PlayerHP>();
        //hp = player.GetComponent<PlayerHP>();
        hp = MasterStaticScript.playerReference.GetComponent<PlayerHP>();
    }

    private void OnTriggerEnter(Collider col)
    {
       // if(hp == null) hp = MasterStaticScript.playerReference.GetComponent<PlayerHP>();
        if (col.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("pickup");
            Debug.Log(type);
            hp.CollectedPickUp(type);
            Destroy(this.gameObject);
        }
    }
}
