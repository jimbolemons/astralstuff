using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ImpAttackFire : Gun
{
    public GameObject claw;
    public ImpAnimCOn impAnim;
    // EnemyTarget enimtarg;
    float speed;
    NavMeshAgent agent;

    //public bool canFire = true;

    void Start()
    {
        //enimtarg = GetComponentInParent<EnemyTarget>();
        agent = GetComponentInParent<NavMeshAgent>();
        speed = agent.speed;

        claw.SetActive(false);
    }

    public override void Fire()
    {
        if (canFire)
        {
            enableClaw();
            impAnim.Attack();

            Invoke("disableClaw", .66f);

            Invoke("resetFire", 1f);
        }
    }

    void resetFire()
    {
        canFire = true;
    }
    void enableClaw()
    {
        canFire = false;
        claw.SetActive(true);
    }
    void disableClaw()
    {
        claw.SetActive(false);
    }
}
