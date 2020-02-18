using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for any object that has a health value, or should stop bullets
/// Additional classes may inherit from this class and add custom values such as the individual unit's health
/// </summary>
public abstract class ObjectWithHealth : MonoBehaviour
{
    //Nan is simply used for a filler to be changed later. 
    //What type of object this is
    public enum objectWithHealthType { player, enemy, destructible, terrain, nan }

    //sets the object's type to a default value to be overridden later
    public objectWithHealthType objectType = objectWithHealthType.nan;
    [Tooltip("Object's max health. Make sure to make this positive if the object can take damage.")]
    public float health = -1;
    [Tooltip("Whether or not the object can die")]
    public bool immortal = false;

    bool canTakeDamage = true;
    float timer =.1f;
    float InvolnTime = 1f;


    //TODO: Needs separate variable for default health value if the objects able to heal or show a health bar

    /// <summary>
    /// Calculate damage to health
    /// </summary>
    /// <param name="damage">how much damage the object is taking</param>
    
    public void TakeDamage(float damage)
    {
        if (canTakeDamage)
        {
            TriggerOnDamage();
            health -= damage;
       
            canTakeDamage = false;
        }

        if ((health <= 0) && !immortal)
        {
            TriggerOnDeath();
        }
    }
    public void LateUpdate()
    {
        Timers();
    }
    public void Timers()
    {
        if (!canTakeDamage)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            canTakeDamage = true;
            timer = InvolnTime;
        }
        
    }
    /// <summary>
    /// Anything that needs to start when the object dies
    /// </summary>
    public abstract void TriggerOnDeath();

    public abstract void TriggerOnDamage();
    /// <summary>
    /// reference to object's type
    /// </summary>
    /// <param name="typeToSet"></param>
    public void SetParent(objectWithHealthType typeToSet)
    {
        objectType = typeToSet;
    }
}
