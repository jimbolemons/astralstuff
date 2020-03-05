using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ObjectWithHealth Class
/// Default enemy health stats
/// </summary>
public class EnemyHP : ObjectWithHealth
{
    public CameraShake cameraShake;
    public GameObject cameras;
    public AudioSource slap;
    public EnemyTarget eneeie;
    public AutoFire shoot;


    ImpAnimCOn impAnim;
    BruteAnimCon bruteAnim;
    conjAnimCon conjAnim;
    private void Start()
    {
        objectType = objectWithHealthType.enemy;
        MasterStaticScript.enemyList.Add(gameObject);
        //print("Enemies currently in Array: " + MasterStaticScript.enemyList.Count);
        cameras = GameObject.Find("/cameraHolder/Camera");
        cameraShake = cameras.GetComponent<CameraShake>();
        impAnim = gameObject.GetComponentInChildren<ImpAnimCOn>();
        bruteAnim = gameObject.GetComponentInChildren<BruteAnimCon>();
        conjAnim = gameObject.GetComponentInChildren<conjAnimCon>();
        eneeie = this.GetComponent<EnemyTarget>();
        shoot = gameObject.GetComponentInChildren<AutoFire>();
    }
   


    public override void TriggerOnDeath()
    {
        if (impAnim != null)
            impAnim.dead = true;
        if (bruteAnim != null)
            bruteAnim.dead = true;
        if (conjAnim != null)
            conjAnim.dead = true;

        shoot.enabled = false;
        eneeie.enabled = false;
        Invoke("Death", 5f);
        
        
    }
    public override void TriggerOnDamage()
    {
        //StartCoroutine(cameraShake.Shake(.15f, .4f));
        //FindObjectOfType<AudioManager>().Play("SLAP");
        slap.Play();
        Debug.Log("ouch");
    }
    private void Death()
    {
        MasterStaticScript.enemyList.Remove(gameObject);
        Destroy(gameObject);
    }
}
