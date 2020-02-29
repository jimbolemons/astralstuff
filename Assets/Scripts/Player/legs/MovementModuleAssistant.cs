using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is the container / handler for the player's ability to swap movement modules
/// If the player wants to swap modules, it handles passing in the default/required information and references
/// If the player is unable to swap modules, this script is unnecessary
/// </summary>
public class MovementModuleAssistant : MonoBehaviour
{
    //default parameters to pass on
    public Transform cameraTarget;
    public Transform player;
    public CharacterController controller;

    //reference to base movement module just in case player is ever without one
    public BaseMovementModule movementModule;


    float defaultModuleTimer;
    public float defaultModuleTimerBase = 2;

    void Start()
    {
        defaultModuleTimer = defaultModuleTimerBase;
        BaseMovementModule baseMod = GetComponent<BaseMovementModule>();
        if (baseMod != null) SetModule(baseMod);
    }

    void Update()
    {
        //make sure there is at least one movement module so the player can move
        //
        if (movementModule == null)
        {
            //To handle Unity's garbage collection, a short delay is required to perform a swap
            defaultModuleTimer -= Time.deltaTime;
            if(defaultModuleTimer <= 0)
            {
               BaseMovementModule defaultModuleReference = gameObject.AddComponent<BaseMovementModule>();
                SetModule(defaultModuleReference);
                defaultModuleTimer = defaultModuleTimerBase;
            }
        }
        else
        {
            defaultModuleTimer = defaultModuleTimerBase;
        }
    }

    /// <summary>
    /// Pass on the needed references to the new module
    /// </summary>
    /// <param name="m"></param>
    void InformModule(BaseMovementModule m)
    {
        movementModule.SetPlayer(player);
        movementModule.SetCameraTarget(cameraTarget);
        movementModule.SetCharacterController(controller);
    }
    /// <summary>
    /// function called to assign / equip the new module
    /// </summary>
    /// <param name="b"></param>
    void SetModule(BaseMovementModule b)
    {
        movementModule = b;
        InformModule(movementModule);
    }
}
