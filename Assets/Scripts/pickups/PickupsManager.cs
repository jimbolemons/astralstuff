using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsManager : MonoBehaviour
{


    public bool hasPower = false;
    public bool hasSpeed = false;
    public bool hasHealth = false;

    public float healAmount = 5;


    public bool usingSpeed = false;
    public bool usingPower = false;
    bool usingSpeed2 = false;
    bool usingPower2 = false;

    public BulletDamage heavyAttack;
    public BulletDamage lightAttack;
    public BulletDamage airAttack;
   
    public float powerBoostAmt;

    float starthealth;

    public MovModDoubleJump hopeLegs;
    public MovModDoubleJump wafflesLegs;
    public float speedBoostAmt;

    public float speedTimer;
    public float powerTimer;

    
    

    float speedTimer2;
    float powerTimer2;


    public GameObject player;
   
    // Start is called before the first frame update
    void Start()
    {
        starthealth = player.GetComponent<PlayerHP>().health;
        speedTimer2 = speedTimer;
        powerTimer2 = powerTimer;
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

        BoostPower();
        BoostSpeed();

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
            powerTimer2 = powerTimer;
            usingPower = true;
            hasPower = false;
            // give the player a power boost
            Debug.Log("POOOWWWAAAAA");

        }
    }
    public void UseSpeed()
    {
        if (hasSpeed)
        {

            speedTimer2 = speedTimer;
            usingSpeed = true;
            hasSpeed = false;

            //giv ethe player a speed boost
            Debug.Log("SPEEEEEEEED");
        }
    }
    public void UseHealth()
    {
        if (hasHealth)
        {

            player.GetComponent<PlayerHP>().Heal(healAmount);
            //give the player some health
            Debug.Log("You dawg you got any more of that Health?");
            hasHealth = false;

        }
    }
    public void BoostPower()
    {
        if (usingPower)
        {

           // heavyAttack.damage += powerBoostAmt;
            //lightAttack.damage += powerBoostAmt;
            //airAttack.damage += powerBoostAmt;

           // usingPower = false;
            usingPower2 = true;

            //increase power here


        }
        if (usingPower2 && powerTimer2 <= 0)
        {
            heavyAttack.damage -= powerBoostAmt;
            lightAttack.damage -= powerBoostAmt;
            airAttack.damage -= powerBoostAmt;
            //decreas power here
            usingPower2 = false;
            usingPower = false;
        }
        if (powerTimer2 > 0)
        {
            powerTimer2 -= Time.deltaTime;
        }
    }
    public void BoostSpeed()
    {
        if (usingSpeed)
        {
            //increase speed here
            hopeLegs.speed += speedBoostAmt;
            wafflesLegs.speed += speedBoostAmt;
            usingSpeed = false;
            usingSpeed2 = true;
        }
       if (usingSpeed2 && speedTimer2 <= 0)
        {
            hopeLegs.speed -= speedBoostAmt;
            wafflesLegs.speed -= speedBoostAmt;
            //decrease speed here

            usingSpeed2 = false;
        }
        if (speedTimer2 > 0)
        {
            speedTimer2 -= Time.deltaTime;
        }

    }
    
}
