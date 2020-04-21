using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemonAttackFire : MonoBehaviour
{
    public Gun gun;
    public ImpAnimCOn impAnim;
   // EnemyTarget enimtarg;
    float speed;
    NavMeshAgent agent;


void Start()
{
    //enimtarg = GetComponentInParent<EnemyTarget>();
     agent = GetComponentInParent<NavMeshAgent>();
    speed =  agent.speed;
 //impAnim = get
}
    public void Fire()
    {
        //print("firing demon gun");
        
        try
        {
            //AINIM
            // stop the enimy from moving well attacking
        impAnim.Attack();
        gun.Fire();
        agent.speed = 0;
        Invoke("Move", 1f);


        }
        catch
        {
            Debug.Log("someone doesnt have an attack animaton");

        }
        
    }
    public void Move()
    {
     agent.speed = speed;

    }
}
