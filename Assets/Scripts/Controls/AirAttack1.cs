using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirAttack1 : MonoBehaviour
{

    [SerializeField]
    Gun[] AirGuns;
    public float pFiring = 0;
    public CharacterController car;
    public GameObject player;
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
            Debug.Log(car.isGrounded);
           //checks if the playe ris grounded if not then continue
            if (!IsGrounded.Grounded)
            {
                // checks to see if the player is trying to fire if so continues
                if (Input.GetAxis("Fire1") > 0 && pFiring == 0)
                {
                    //fire every gun that hand is holding
                    foreach (Gun g in AirGuns)
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
    }

    /// <summary>
    /// destroy all guns in the array
    /// </summary>
    public void ClearGunList()
    {
        foreach (Gun g in AirGuns)
        {
            Object.Destroy(g.gameObject);
        }
    }

    /// <summary>
    /// gets a reference to every gun object currently held in the hand
    /// </summary>
    public void UpdateGuns()
    {
        AirGuns = GetComponentsInChildren<Gun>();
    }
   
}
