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
    public Transform player;
    public float distanceToPlayer = 20;
    NavMeshAgent agent;

    public enum EnemyState {IDLE, FOLLOW_PLAYER};

    public EnemyState currentState = EnemyState.IDLE;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        switch (currentState)
        {
            case (EnemyState.IDLE):
                if (dist <= distanceToPlayer) currentState = EnemyState.FOLLOW_PLAYER;
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
                        agent.SetDestination(player.position);
                        transform.LookAt(player.position);
                        transform.rotation *= Quaternion.Euler(0, -90, 0);
                    }

                if (dist > distanceToPlayer) currentState = EnemyState.IDLE;
                break;
        }
    }
}
