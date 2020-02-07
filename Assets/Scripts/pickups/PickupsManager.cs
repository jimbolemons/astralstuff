using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsManager : MonoBehaviour
{


    public bool hasPower = false;
    public bool hasSpeed = false;
    public bool hasHealth = false;   

    public GameObject player;
   
    // Start is called before the first frame update
    void Start()
    {      
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            UseCurrentPickup();
        }
            //uncoment this to make the power up go off instantly
        //UseCurrentPickup();

    }
    public void UseCurrentPickup()
    {
        Debug.Log("Attempt use pickup");
        if (hasPower)
            UsePower();        
        else if (hasSpeed)
            UseSpeed();
        else if (hasHealth)
            UseHealth();
    }
    void SetAllFalse()
    {
        
        hasPower = false;        
        hasSpeed = false;
        hasHealth = false;
    }
    public void CollectedPickUp(PickupType type)
    {
        switch (type)
        {
            case PickupType.power:
                SetAllFalse();
                hasPower = true;
                Debug.Log("has power = " + hasPower);
                break;             
            case PickupType.speed:
                SetAllFalse();
                hasSpeed = true;
                Debug.Log("has speed = " + hasSpeed);
                break;
            case PickupType.health:
                SetAllFalse();
                hasHealth = true;
                Debug.Log("has health = " + hasHealth);
                break;
        }
    }

    public void UsePower()
    {
        if (hasPower)
        {
            // give the player a power boost
            Debug.Log("POOOWWWAAAAA");

        }
    }
    public void UseSpeed()
    {
        if (hasSpeed)
        {
            //giv ethe player a speed boost
            Debug.Log("SPEEEEEEEED");
        }
    }
    public void UseHealth()
    {
        if (hasHealth)
        {
            //give the player some health
            Debug.Log("You dawg you got any more of that Health?");

        }
    }
    
}
