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
    public float maxHealth =0;
    public float health;
    [Tooltip("Whether or not the object can die")]
    public bool immortal = false;

    bool canTakeDamage = true;
    float timer =.5f;
    float InvolnTime = .5f;

    //public Material[] materials;
    public Material baseMat;
    public Material lerpMat;

    //public Material color2Mat;
    public List<Material> baseMats;


    public SkinnedMeshRenderer[] testRenderers;
    //public Renderer testRenderer;
    
    //public Color baseColor;
    public Material flashMat;



    private void Start()
    {
        if (maxHealth == 0) health = -1;
        health = maxHealth;
        Debug.Log(testRenderers.Length);
        GetRenderers();
    }

    public void GetRenderers()
    {
        testRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        //testRenderer = testRenderers[0];

        // foreach(Renderer r in testRenderers)
        // {
        //     baseMats.Add(r.materials[0]);
        // }
        for (int i = 0; i < testRenderers.Length; i++)
        {
            baseMats.Add(testRenderers[i].material);
        }

        //materials = testRenderers.materials;
        baseMat = baseMats[0];
        //color2Mat = materials[1];
        lerpMat = baseMat;
    }
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

            //lerp material color
          //  foreach(Material m in materials)
          //  {
          //      m = m.Lerp(m, flashMat, InvolnTime - timer);
          //  }

           // for(int i = 0; i < materials.Length; i++)
           // {
           //     materials[i].Lerp(materials[i], flashMat, InvolnTime - timer);
           // }
           
            try
            {
                //print("trying to lerp");
                //lerpMat.Lerp(baseMat, flashMat, timer);
               
               // testRenderer.material = lerpMat;      
               
                //NOTICE!!: if there are errors / the material is not switching back
                //check and see if the GetRenderers() function is being called in start
                //can't swap to a material you don't have a reference to!
               foreach(Renderer r in testRenderers)
                {
                    r.material = flashMat;
                }            
            }
            catch
            {

            }

        }
        if (timer < 0)
        {
            canTakeDamage = true;
            timer = InvolnTime;
            try
            {
                //testRenderer.material = baseMat;
                for (int i = 0; i < testRenderers.Length; i++)
                {
                    testRenderers[i].material = baseMats[i];
                }
               // foreach (Renderer r in testRenderers)
               // {
               //     r.material = baseMat;
               // }
            }
            catch
            {

            }
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
