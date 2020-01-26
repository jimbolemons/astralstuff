using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Navmesh script to path towards the player. Last edited by Kyle on 1.13.2020
/// </summary>
public class EnemyTarget : MonoBehaviour
{

    [Tooltip("Reference to the player.")]
    public float distanceToPlayer = 20;
    NavMeshAgent agent;
    private Transform targetSite;
    private bool readyToGo = false;

    public enum EnemyState {IDLE, FOLLOW_PLAYER, FOLLOW_SITE};

    public EnemyState currentState = EnemyState.IDLE;

    void Start()
    {       
        agent = GetComponent<NavMeshAgent>();               
    }

    void Update()
    {
        Vector3 forwardTransform = transform.position;

        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0] ;

        float dist = Vector3.Distance(player.transform.position, transform.position);  ////MasterStaticScript.player.position
        switch (currentState)
        {
            case (EnemyState.IDLE):
                try
                {
                agent.SetDestination(transform.position);
                }
                catch
                {                  
                }
                forwardTransform = transform.position;
                transform.LookAt(forwardTransform);
                if (dist <= distanceToPlayer) currentState = EnemyState.FOLLOW_PLAYER;
                if (readyToGo) currentState = EnemyState.FOLLOW_SITE;
                break;
            case (EnemyState.FOLLOW_PLAYER):
                    //only move if game is not paused
                    if (MasterStaticScript.gameIsPaused)
                    {
                        agent.isStopped = true;
                    }
                    else
                    {
                        //path towards target
                        agent.isStopped = false;
                        agent.SetDestination(player.transform.position);    //MasterStaticScript.player.position
                        transform.LookAt(player.transform.position);        //MasterStaticScript.player.position
                        transform.rotation *= Quaternion.Euler(0, -90, 0);
                        //print("Demon is following player.");
                    }

                if (dist > distanceToPlayer) currentState = EnemyState.IDLE;
                if (readyToGo && dist > distanceToPlayer) currentState = EnemyState.FOLLOW_SITE;
                break;
            case (EnemyState.FOLLOW_SITE):
                //only move if game is not paused
                if (MasterStaticScript.gameIsPaused)
                {
                    agent.isStopped = true;
                }
                else
                {
                    //path towards target
                    agent.isStopped = false;
                    agent.SetDestination(targetSite.position);    //MasterStaticScript.player.position
                    transform.LookAt(targetSite.position);        //MasterStaticScript.player.position
                    transform.rotation *= Quaternion.Euler(0, -90, 0);
                    //print("Demon is moving towards Sacred Site.");
                }

                if (dist <= distanceToPlayer) currentState = EnemyState.FOLLOW_PLAYER;
                //TODO: refine "back to IDLE" for state machine.
                //else currentState = EnemyState.IDLE;
                break;
        }
    }

    public void SetTarget(Transform target)
    {
        targetSite = target;
        readyToGo = true;
        currentState = EnemyState.FOLLOW_SITE;
    }
}
