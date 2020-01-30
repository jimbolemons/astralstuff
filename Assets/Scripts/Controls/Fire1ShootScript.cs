﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Primary shooting script (right hand)
/// Gets a reference to every gun held in the hand, then enables them all to be fired at once
/// </summary>
public class Fire1ShootScript : MonoBehaviour
{
    [SerializeField]
    Gun[] rightArmGuns;
    public float pFiring = 0;
    // Start is called before the first frame update
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
            //If player presses fire1 and was not already holding it
            if (Input.GetAxis("Fire1") > 0 && pFiring == 0)
            {
                //fire every gun that hand is holding
                foreach (Gun g in rightArmGuns)
                {
                    if (g != null) g.Fire();
                }
                pFiring = 1;
            }
            else
            {
                //if the player is not holding fire, enable firing
                if (Input.GetAxis("Fire1") == 0) pFiring = 0;
            }
        }
    }

    /// <summary>
    /// destroy all guns in the array
    /// </summary>
    public void ClearGunList()
    {
        foreach (Gun g in rightArmGuns)
        {
            Object.Destroy(g.gameObject);
        }
    }

    /// <summary>
    /// gets a reference to every gun object currently held in the hand
    /// </summary>
    public void UpdateGuns()
    {
        rightArmGuns = GetComponentsInChildren<Gun>();
    }
}
