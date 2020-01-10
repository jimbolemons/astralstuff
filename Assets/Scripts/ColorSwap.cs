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
    public static bool isWaffles = false;
    public Material playerMat;
    public Material demonMat;
    public MeshRenderer player;
    public MeshRenderer gun;
    public MeshRenderer eyes;
    public MeshRenderer demon;
    public bool counter = false;
    GameObject dummy;
    public GameObject playerTarget;
    public GameObject dummyBase;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject cam;
    
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
            //TODO: Spawn demon with its own player controler and set the camera target to be that object. demon needs to inhearit some items from hope such as health.
            if (counter == true)
            {
                //spawn demon prefab
                dummy = Instantiate(dummyBase, transform.position, Quaternion.identity);

                //set cammera target to demon                 
                cam.GetComponent<ThirdPersonCamera>().target = dummy.gameObject.transform.GetChild(0);                

                //turn off controls for hope
                //TODO
                isWaffles = true;

                //send data from hope to waffles
                //TODO
                                       
            }
            //if swapping to Hope
            //TODO: destroy Demon and set the cameratarget back to hope
            if (counter == false)
            {
                //send data from demon to hope
                //TODO

                //set camera target to hope
                cam.GetComponent<ThirdPersonCamera>().target = playerTarget.gameObject.transform.GetChild(0);

                //turn on hopes controls 
                //TODO
                isWaffles = false;

                //destroy demon prefab
                Destroy(dummy);
                                       
            }
            counter = !counter;
        }
    }
}
