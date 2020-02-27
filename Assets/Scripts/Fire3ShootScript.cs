using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire3ShootScript : MonoBehaviour
{
    [SerializeField]
    Gun[] leftArmGuns;
    public float pFiring = 0;
    void Start()
    {
        UpdateGuns();
        //get every Gun script able to fire
        //put into list
    }

    void Update()
    {
        //only fire if not paused
        if (MasterStaticScript.gameIsPaused == false)
        {
            //if player presses fire2
            if (Input.GetKeyDown(KeyCode.J) && pFiring == 0)
            {
                //fire every gun the hand is holding
                foreach (Gun g in leftArmGuns)
                {
                    if (g != null) g.Fire();
                }
                pFiring = 1;
            }
            else
            {
                //if the player is not holding fire, enable firing
                //if(Input.GetAxis("Fire3") == 0) pFiring = 0;
                if (Input.GetKeyDown(KeyCode.J)) pFiring = 0;
                //fire every gun the hand is holding               

                if (Input.GetKeyUp(KeyCode.J))
                {
                    foreach (Gun g in leftArmGuns)
                    {
                        if (g != null) g.StopFire();
                    }
                }
            }
        }
    }
    /// <summary>
    /// destroy all guns in the current array
    /// </summary>
    public void ClearGunList()
    {
        foreach (Gun g in leftArmGuns)
        {
            Object.Destroy(g.gameObject);
        }
    }
    /// <summary>
    /// gets a reference to every gun object currently held in the hand
    /// </summary>
    public void UpdateGuns()
    {
        leftArmGuns = GetComponentsInChildren<Gun>();
    }
}