using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Navmesh script to path towards the player
/// </summary>
public class PlayerTarget : MonoBehaviour
{
    [Tooltip("Reference to the player.")]
    public Transform player;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
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



    }
}
