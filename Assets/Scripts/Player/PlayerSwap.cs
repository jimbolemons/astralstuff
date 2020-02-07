using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is a very janky script that swaps the player's model with the demon and enables/disables corresponding items
/// </summary>
public class PlayerSwap : MonoBehaviour
{
    //TODO: Refactor this to be less of a mess
    //Reference to all of the objects that need to be swapped or deactivated
    public static bool isWaffles = false;   
    public bool counter = true;
    GameObject dummy;
    public GameObject playerTarget;
    public GameObject dummyBase;   
    public GameObject cam;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //if swapping to demon
            //TODO: Spawn demon with its own player controler and set the camera target to be that object. demon needs to inhearit some items from hope such as health.
            if (counter == true)
            {
                //spawn demon prefab / or move the demon to the players position
                dummy = Instantiate(dummyBase, transform.position, Quaternion.identity);

                //set cammera target to demon                 
                cam.GetComponent<ThirdPersonCamera>().target = dummy.gameObject.transform.GetChild(0);

                //turn off controls for hope                
                isWaffles = true;
                playerTarget.GetComponent<CharacterController>().enabled = false;
                playerTarget.GetComponent<Hook>().enabled = false;

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
                isWaffles = false;
                playerTarget.GetComponent<CharacterController>().enabled = true;
                playerTarget.GetComponent<Hook>().enabled = true;

                //destroy demon prefab
                Destroy(dummy);

            }
            counter = !counter;
        }
    }
}
