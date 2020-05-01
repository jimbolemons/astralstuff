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
    [HideInInspector]
    public GameObject player;
    public float distanceToPlayer = 20;
    //more anim stuff

    ImpAnimCOn impAnim;
    BruteAnimCon bruteAnim;
    conjAnimCon conjAnim;

    public float moveSpeed;
    public float rotSpeed = 1;


    public Gun gunReference;

    public Gun hiddenGun;
    //float step;    

    NavMeshAgent agent;
    public Transform targetSite;
    private bool readyToGo = false;

    public float attackRange = 5;

    public float bonusAttackRange = 0;

    public enum EnemyState { IDLE, FOLLOW_PLAYER, FOLLOW_SITE };

    public EnemyState currentState = EnemyState.IDLE;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        //animstuf go here
        impAnim = gameObject.GetComponentInChildren<ImpAnimCOn>();
        bruteAnim = gameObject.GetComponentInChildren<BruteAnimCon>();
        conjAnim = gameObject.GetComponentInChildren<conjAnimCon>();
    }

    void Update()
    {
        
        try
        {
            if (targetSite != null)
            {
                FindNearestSite();
                //print("Site is fine");
            }
          }
          catch
          {
         
              print("Try not working!");
          }
    }

    private void LateUpdate()
    {
        EnemyStateMachine();
        if (!MasterStaticScript.gameIsPaused)
        {
            currentState = EnemyState.IDLE;
        }
      }

    //debug helper that draws the nav mesh path when demon is selected
    void OnDrawGizmosSelected()
    {        
        if (agent == null || agent.path == null)
            return;

        var line = this.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = this.gameObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default")) { color = Color.yellow };
            line.SetWidth(0.5f, 0.5f);
            line.SetColors(Color.yellow, Color.yellow);
        }

        var path = agent.path;

        line.SetVertexCount(path.corners.Length);

        for (int i = 0; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]);
        }

    }
    void EnemyStateMachine()
    {
        Vector3 forwardTransform = transform.position;

        float dist = Vector3.Distance(player.transform.position, transform.position);  ////MasterStaticScript.player.position
        if (dist <= distanceToPlayer) currentState = EnemyState.FOLLOW_PLAYER;
        if (readyToGo && dist >= distanceToPlayer) currentState = EnemyState.FOLLOW_SITE;

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
                //Anims Idle
                if(impAnim != null)
                impAnim.run = false;
                if (bruteAnim != null)
                    bruteAnim.run = false;
                if (conjAnim != null)
                    conjAnim.run = false;
               
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
                    //Anims Walking
                    if (impAnim != null)
                        impAnim.run = true;
                    if (bruteAnim != null)
                        bruteAnim.run = true;
                    if (conjAnim != null)
                        conjAnim.run = true;
                    //path towards target
                    agent.isStopped = false;
                    agent.SetDestination(player.transform.position);    //MasterStaticScript.player.position

                    var neededRotation2 = Quaternion.LookRotation(player.transform.position - transform.position);
                  Quaternion.Slerp(transform.rotation, neededRotation2  , Time.deltaTime * rotSpeed);

                    
                    //print("trying to fire" + checkDistance(player.transform.position, attackRange));
                    if (checkDistance(player.transform.position, attackRange))
                    {
                        //print("firing");
                        gunReference.Fire();
                    }

                    // transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));        //MasterStaticScript.player.position
                    //transform.LookAt(player.transform.position);       
                    //TODO: face gun / attack towards player y, not whole model
                    //transform.rotation *= Quaternion.Euler(0, -90, 0);
                    //print("Demon is following player.");
                }

                if (dist > distanceToPlayer)
                {
                    readyToGo = true;
                    currentState = EnemyState.IDLE;                    
                }
                break;
            case (EnemyState.FOLLOW_SITE):
                //only move if game is not paused
                if (MasterStaticScript.gameIsPaused)
                {
                    agent.isStopped = true;
                }
                else
                {
                    //ANims Walking
                    if (impAnim != null)
                        impAnim.run = true;
                    if (bruteAnim != null)
                        bruteAnim.run = true;
                    if (conjAnim != null)
                        conjAnim.run = true;
                    //path towards target
                    agent.isStopped = false;                    
                    try
                    {
                       // Debug.Log(agent.SetDestination(targetSite.position));                       
                        agent.SetDestination(targetSite.position);    //MasterStaticScript.player.position
                        transform.LookAt(targetSite.position);        //MasterStaticScript.player.position

                        var neededRotation3 = Quaternion.LookRotation(targetSite.transform.position - transform.position);
                        Quaternion.Slerp(transform.rotation, neededRotation3, Time.deltaTime * rotSpeed);

                        //transform.rotation *= Quaternion.Euler(0, -90, 0);

                        //print("trying to fire" + checkDistance(targetSite.transform.position, attackRange));
                        if (checkDistance(targetSite.transform.position, attackRange + bonusAttackRange))
                        {
                            agent.isStopped = true;
                            //print("firing");
                            gunReference.Fire();

                            if(hiddenGun!= null) hiddenGun.Fire();
                        }
                    }
                    //print("Demon is moving towards Sacred Site.");
                    catch
                    {
                        //Debug.Log("finding new site");
                        FindNearestSite();
                    }
                }

                //TODO: refine "back to IDLE" for state machine.
                //else currentState = EnemyState.IDLE;
                break;
        }
    }

    public bool checkDistance(Vector3 targetLocation, float distance)
    {
        float distanceFromTarget = Vector3.Distance(targetLocation, transform.position);

        if(distanceFromTarget < distance)
        {
            return true;
        }
        return false;
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
        if (closestSite != null)
        { 
            SetTarget(closestSite.transform);
        }
        //2.) Output the final gate's transform to SacredTarget.

    }
}
