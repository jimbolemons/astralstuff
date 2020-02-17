using System.Collections;
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
    public CharacterController car;
    public float holdBeforNewAttackTime;
    public float attackTimer;
    public GameObject player;

    public CharacterController controller;
    public float speed;
    public Vector3 direction;
    bool attackDash = false;
    float timer ;
    public float attackDashTime = 1;
    // Start is called before the first frame update
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
            //checks to see if the player is grounded if they are then continue
            if (IsGrounded.down)
            {
                // if the player is holding down the left mouse button
                if (Input.GetMouseButton(0)) 
                {                    
                    if (attackTimer == 0)
                    {
                        foreach (Gun g in rightArmGuns)
                        {
                            if (g != null)
                            {
                              g.Fire();
                                // move player forword
                                attackDash = true;
                               

                            }
                            
                        }

                    }
                    attackTimer += Time.deltaTime;
                }
                //if they are still holding down the mouse button and the timer has gone up enough
                if (Input.GetMouseButton(0) && attackTimer >= holdBeforNewAttackTime)
                {
                    // fires each gun in the hands
                    foreach (Gun g in rightArmGuns)
                    {
                        if (g != null) g.Fire();
                    }
                    attackTimer = 0;

                }
                if (Input.GetMouseButtonUp(0) && attackTimer < holdBeforNewAttackTime)
                {
                    attackTimer = 0;

                }

                /*If player presses fire1 and was not already holding it
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
                */
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
        else {
            attackDash = false;
            timer = attackDashTime;
        }
    }
    
}
