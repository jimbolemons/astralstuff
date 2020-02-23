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
    public Transform targetSite;
    private bool readyToGo = false;

    public enum EnemyState { IDLE, FOLLOW_PLAYER, FOLLOW_SITE };

    public EnemyState currentState = EnemyState.IDLE;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
          try
          {
              if (targetSite != null) print("Site is fine");
          }
          catch
          {
              FindNearestSite();
              print("Try not working!");
          }
    }

    private void LateUpdate()
    {
        EnemyStateMachine();
    }

    void EnemyStateMachine()
    {
        Vector3 forwardTransform = transform.position;

        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];

        float dist = Vector3.Distance(player.transform.position, transform.position);  ////MasterStaticScript.player.position
        if (dist <= distanceToPlayer) currentState = EnemyState.FOLLOW_PLAYER;
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
                    transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));        //MasterStaticScript.player.position
                    //transform.LookAt(player.transform.position);       
                    //TODO: face gun / attack towards player y, not whole model
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
                    try
                    {
                        agent.SetDestination(targetSite.position);    //MasterStaticScript.player.position
                        transform.LookAt(targetSite.position);        //MasterStaticScript.player.position
                        transform.rotation *= Quaternion.Euler(0, -90, 0);
                    }
                    //print("Demon is moving towards Sacred Site.");
                    catch
                    {
                        Debug.Log("finding new site");
                        FindNearestSite();
                    }
                }


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

    //Checks all the sites in the level, and sets the closest one as targetSite
    public void FindNearestSite()
    {
        //1.) Check every site's distance to this gate.
        GameObject closestSite = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject site in MasterStaticScript.sacredSites)
        {
            Vector3 difference = site.transform.position - position;
            float currentDistance = difference.sqrMagnitude;
            if (currentDistance < distance)
            {
                closestSite = site;
                distance = currentDistance;
            }
        }
        SetTarget(closestSite.transform);
        //2.) Output the final gate's transform to SacredTarget.

    }
}
