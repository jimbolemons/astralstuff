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
    public float pFiring = 0;
    public float chargeTimer = 0;
    public float attackTime = 1;
    public float attackRelese = 1;
    public GameObject player;

    public CharacterController controller;
    public float speed;
    public Vector3 direction;
    bool attackDash = false;
    float timer;
    public float attackDashTime = 1;

    public AudioManager audioManager;

    void Start()
    {
        UpdateGuns();
        //get every Gun script able to fire
        //put into list
        timer = attackDashTime;
    }

    void Update()
    {
        AttackDash();
        //only fire if not paused
        if (MasterStaticScript.gameIsPaused == false)
        {
            // if the player is grounded
            if (IsGrounded.down)
            {
                //if player presses fire2. starts a timer 
                if (Input.GetAxis("Fire2") > 0 && pFiring == 0)
                {
                    chargeTimer += Time.deltaTime;

                }
                // if the player has held down the button long enough then auto attack
                if (Input.GetMouseButton(1) && chargeTimer >= attackRelese)
                {
                    //FindObjectOfType<AudioManager>().Play("SLAP");
                    //fire every gun the hand is holding
                    foreach (Gun g in leftArmGuns)
                    {
                        if (g != null)
                        {
                            g.Fire();
                            attackDash = true;
                        }
                    }
                    chargeTimer = 0;
                }
                // if the player has held down the  button long enogh befor releseing then fire the 
                if ((Input.GetMouseButtonUp(1)) && (chargeTimer > attackTime))
                {
                    //fire every gun the hand is holding
                    foreach (Gun g in leftArmGuns)
                    {
                        if (g != null) g.Fire();
                    }
                    chargeTimer = 0;
                }
                // if the player releses too early cancle every thing and reset
                if ((Input.GetMouseButtonUp(1)) && (chargeTimer < attackTime))
                {
                    chargeTimer = 0;
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
    public void AttackDash()
    {
        if (attackDash)
        {
            direction = 5 * player.transform.forward;
            //direction += Input.GetAxis("Horizontal") * transform.right;
            direction *= speed;
            controller.Move(direction * Time.deltaTime);
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;

        }
        else
        {
            attackDash = false;
            timer = attackDashTime;
        }
    }
}
