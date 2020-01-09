using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class for all guns
/// contains default stats and references
/// All guns should inherit from this class and add in custom information
/// </summary>
public abstract class Gun : MonoBehaviour
{
    [Tooltip("Where the bullets come out from.")]
    public Transform gunEnd;
     [Tooltip("The bullet the gun fires.")]
    public GameObject projectilePrefab;
    [Tooltip("Time between shots.")]
    public float fireCooldown = .3f;
    //whether or not the gun is ready to fire
    public bool canFire = true;

    //reference to gun's parent (whoever is shooting it)
    public ObjectWithHealth.objectWithHealthType parentType;

    private void Start()
    {
        //initialize parent type
        parentType = GetComponentInParent<ObjectWithHealth>().objectType;
    }

    /// <summary>
    /// Fire function to be called and set up based on each individual gun's needs
    /// </summary>
    public abstract void Fire();
    /// <summary>
    /// Reset the gun to be ready to fire (after cooldown is done)
    /// </summary>
    public void ResetFire()
    {
        canFire = true;
    }

}
