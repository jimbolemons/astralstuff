using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadokenControl : MonoBehaviour
{
    public float size;
    public BulletStats stats;
    public BulletMovement movement;

    Transform holderTransform;

    public float power = 1;
    
    // Start is called before the first frame update
    void Start()
    {
      
        stats = GetComponentInChildren<BulletStats>();
        
        try
        {
        movement = GetComponent<BulletMovement>();
        }
        catch
        {
        movement = GetComponentInChildren<BulletMovement>();
        }
        movement.moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPower(float x)
    {
        power = x;
        transform.localScale = new Vector3(power, power, power);
    }
    public void TimeToGo()
    {
        transform.rotation = holderTransform.rotation;
        stats.damage = power;
        movement.speed = 30 - power;
        movement.moving = true;
        transform.parent = null;
    }


    public void SetTransform(Transform t)
    {
        holderTransform = t;
    }
    public void SetParent(GameObject parent)
    {
        transform.parent = parent.transform;
    }
    
}
