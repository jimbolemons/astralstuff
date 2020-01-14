using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Navmesh script to path towards the player. Last edited by Kyle on 1.13.2020
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
        if (state != null)
        {
            
        }
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
