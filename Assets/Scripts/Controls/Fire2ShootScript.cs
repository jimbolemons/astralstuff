using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Secondary shooting script (left hand)
/// Gets a reference to every gun held in the hand, then enables them all to be fired at once
/// </summary>
public class Fire2ShootScript : MonoBehaviour
{
    [SerializeField]
    Gun[] leftArmGuns;
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
            if (Input.GetAxis("Fire2") > 0)
            {
                //fire every gun the hand is holding
                foreach (Gun g in leftArmGuns)
                {
                    if (g != null) g.Fire();
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
