using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stats for bullets called on collision
/// ensures bullet cannot hit same type of object
/// </summary>
public class BulletStats : MonoBehaviour
{
    //how much damage the bullet does
    public float damage = 1;

    //reference to the object that fired the bullet (type)
    public ObjectWithHealth.objectWithHealthType parentType;

    /// <summary>
    /// Assigns the parent's tag to the bullet
    /// </summary>
    /// <param name="type"></param>
    public void SetParent(ObjectWithHealth.objectWithHealthType type)
    {
        parentType = type;
    }
}
