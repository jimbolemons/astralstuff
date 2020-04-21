using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDemons : MonoBehaviour
{
    public Transform SpawnPoint;
    public Collider spawnArea;
    public GameObject imp;
    public GameObject brute;
    public GameObject conjuror;

    [Space(10)]
    public int numberOfDemons = 0;
    public int demonsToSpawn = 5;

    public float secondsBetweenGroups = 10;
    //hide later
    public float countdownForGroup = 0;

    public float secondsToSpawn = 3;
    //hide later
    public float spawnEnergy = 0;

    public Transform SacredTarget;
    public List<GameObject> demonList;

    public float threatLevel = 0;
    public float threatMultiplier = 5;

    public GateHP healthStats;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!MasterStaticScript.gameIsPaused)
        {
            //health check
            if (healthStats.health / healthStats.maxHealth > .66)
            {
                threatLevel = 1;
            }
            else
            if (healthStats.health / healthStats.maxHealth > .33)
            {
                //if the gate just crossed below 2/3
                if (threatLevel == 1)
                {
                    for (int i = 0; i < threatMultiplier/2; i++)
                    {
                        Vector3 demonSpawn = SpawnLocation(spawnArea.bounds);
                        GameObject demon = SpawnDemon(demonSpawn);
                    }
                }
                threatLevel = 2;
            }
            else
            if (healthStats.health / healthStats.maxHealth < .33)
            {
                //if the gate just crossed below 1/3
                if (threatLevel == 2)
                {
                    for (int i = 0; i < threatMultiplier; i++)
                    {
                        Vector3 demonSpawn = SpawnLocation(spawnArea.bounds);
                        GameObject demon = SpawnDemon(demonSpawn);
                    }
                    spawnEnergy += secondsToSpawn * threatMultiplier;
                }

                threatLevel = 3;
            }
            
            //if ready to send out the demons
            if (numberOfDemons >= demonsToSpawn)
            {
                //tell the demons to go
                SendOutTheDemons();


                //clear the references
                demonList.Clear();


                //start counting for delay between spawns
                countdownForGroup += Time.deltaTime;
                //if countdown is done
                if (countdownForGroup > secondsBetweenGroups)
                {
                    countdownForGroup = 0;
                    numberOfDemons = 0;
                }
            }

            //if spawning demons
            if (numberOfDemons < demonsToSpawn)
            {
                spawnEnergy += Time.deltaTime;
                if (spawnEnergy > secondsToSpawn)
                {
                    Vector3 demonSpawn = SpawnLocation(spawnArea.bounds);         
                    GameObject demon = SpawnDemon(demonSpawn);
                    //SetDemonTarget(demon);
                    numberOfDemons++;
                    spawnEnergy -= secondsToSpawn;
                    demonList.Add(demon);
                }
            }
            //TODO: something if the sacred site is destroyed
        

            //TODO: spawn facing sacred site

            //TODO: spawn in larger area?

            //TODO: different demons take different time / energy?
        }
        
    }
  
    public void SendOutTheDemons()
    {
        foreach (GameObject g in demonList)
        {
            SetDemonTarget(g);
        }
    }

    Vector3 SpawnLocation(Bounds bounds)
    {
        return new Vector3(Random.Range(bounds.min.x, bounds.max.x),
            0,
            Random.Range(bounds.min.z, bounds.max.z));
    }

    void SetDemonTarget(GameObject demon)
    {
        //set the demon's target
        try
        {
            EnemyTarget targetScript = demon.GetComponent<EnemyTarget>();
            targetScript.SetTarget(SacredTarget);
        }
        catch
        {
            Debug.LogError("Enemy unable to set valid target!!");
        }
    }

    GameObject SpawnDemon(Vector3 location)
    {
        GameObject demon = Instantiate(RandomDemon(), location, Quaternion.identity);
        return demon;
    }
    GameObject RandomDemon()
    {
        int number = Random.Range(1, 4);
        if (number == 1)
        {
            return brute;
        }
        else if (number == 2)
        {
            return conjuror;
        }
        else
        {
            return imp;
        }
    }
}
