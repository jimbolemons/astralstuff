using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ObjectWithHealth Class
/// Player's health value
/// </summary>
public class PlayerHP : ObjectWithHealth
{
    public CameraShake cameraShake;
    public PickupsManager pickups;
    public bool isDead = false;
    bool deadNoflash = false;
    public GameObject player;

    public GameObject hope;
    public GameObject deadBody;
    public GameObject waffles;
    
    HopeAnimsController hopeAnims;
     animsWaffles waffleanims;

    private void Start()
    {
        objectType = objectWithHealthType.player;
        GetRenderers();
        hopeAnims =  GetComponentInChildren<HopeAnimsController>();
        waffleanims = GetComponentInChildren<animsWaffles>();
    }

    public void Update()
    {
        if (isDead) killPlayer();
        else
        {
            //do your thing.
        }

        //do this when the playe takes damage.... i dont know where that happens

        //
    }

    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > maxHealth) health = maxHealth;
    }

    public override void TriggerOnDamage()
    {
        if (!deadNoflash)
        {
            hopeAnims.Hit();
            try
            {
            waffleanims.Hit();
            }
            catch
            {

            }
            StartCoroutine(cameraShake.Shake(.15f, .4f));
            PostProcessingEffectsManager.instance.Flash2();
            FindObjectOfType<AudioManager>().Play("wafflesAttackhit");
        }
    }

    public override void TriggerOnDeath()
    {
        isDead = true;

        deadNoflash = true;
        player.GetComponent<CharacterController>().enabled = false;
        deadBody.SetActive(true);
        //deadBody.transform.position = transform.position;
        deadBody.transform.SetParent(null);
        waffles.SetActive(false);
        hope.SetActive(false);

        float number1 = Random.Range(0f, 10f);
        float number2 = Random.Range(0f, 10f);
        float number3 = Random.Range(0f, 10f);
        float number4 = Random.Range(0f, 10f);
        float number5 = Random.Range(0f, 10f);
        float number6 = Random.Range(0f, 5f);


        deadBody.GetComponent<Rigidbody>().AddForce(number1,number6, number2, ForceMode.VelocityChange);
        deadBody.GetComponent<Rigidbody>().AddTorque(number3,number4,number5);
    }

    private void killPlayer()
    {
        print("player down!! Player down!!");
        Invoke("Fail", 2f);
        
        //Destroy(gameObject);
    }
    private void Fail()
    {
        MasterStaticScript.PlayerDead();
    }
    public void CollectedPickUp(PickupType type)
    {
        pickups.CollectedPickUp(type);

    }
}
