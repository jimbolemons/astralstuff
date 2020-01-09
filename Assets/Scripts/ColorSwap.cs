using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is a very janky script that swaps the player's model with the demon and enables/disables corresponding items
/// </summary>
public class ColorSwap : MonoBehaviour
{
    //TODO: Refactor this to be less of a mess
    //Reference to all of the objects that need to be swapped or deactivated
    public Material playerMat;
    public Material demonMat;
    public MeshRenderer player;
    public MeshRenderer gun;
    public MeshRenderer eyes;
    public MeshRenderer demon;
    public bool counter = false;
    GameObject dummy;
    public GameObject dummyBase;
    public GameObject leftHand;
    public GameObject rightHand;
    
    void Start()
    { 
        rightHand.SetActive(true);
        leftHand.SetActive(false);
        demon.enabled = false;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //if swapping to demon
            if (counter == true)
            {
                demon.enabled = true;
                player.enabled = false;
                gun.enabled = false;
                eyes.enabled = false;
                dummy = Instantiate(dummyBase, transform.position, Quaternion.identity);
                rightHand.SetActive(false);
                leftHand.SetActive(true);
            }
            //if swapping to Hope
            if (counter == false)
            {
                demon.enabled = false;
                player.enabled = true;
                gun.enabled = true;
                eyes.enabled = true;
                Destroy(dummy);
                rightHand.SetActive(true);
                leftHand.SetActive(false);
            }
            counter = !counter;
        }
    }
}
