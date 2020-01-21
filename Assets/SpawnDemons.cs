using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDemons : MonoBehaviour
{
    public Transform SpawnPoint;
    public Collider spawnArea;
    public GameObject demon1;


    public int numberOfDemons = 0;
    public int demonsToSpawn = 5;

    public float secondsBetweenGroups = 10;
    //hide later
    public float countdownForGroup = 0;

    public float secondsToSPawn = 3;
    //hide later
    public float spawnEnergy = 0;

    public Transform SacredTarget;
    public List<GameObject> demonList;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if ready to send out the demons
        if(numberOfDemons >= demonsToSpawn)
        {
            //tell the demons to go
            foreach(GameObject g in demonList)
            {
                SetDemonTarget(g);
            }
            //clear the references
            demonList.Clear();


            //start counting for delay between spawns
            countdownForGroup += Time.deltaTime;
            //if countdown is done
            if(countdownForGroup > secondsBetweenGroups)
            {
                countdownForGroup = 0;
                numberOfDemons = 0;
            }
        }

        //if spawning demons
        if(numberOfDemons < demonsToSpawn)
        {
            spawnEnergy += Time.deltaTime;
            if(spawnEnergy > secondsToSPawn)
            {
                Vector3 demonSpawn = SpawnLocation(spawnArea.bounds);
                //print(demonSpawn);
                GameObject demon = SpawnDemon(demonSpawn);
                //SetDemonTarget(demon);
                numberOfDemons++;
                spawnEnergy = 0;
                demonList.Add(demon);
            }                     
        }
        //TODO: something if the sacred site is destroyed

        //TODO: spawn different demons

        //TODO: spawn facing sacred site
        
        //TODO: spawn in larger area?

        //TODO: different demons take different time / energy?
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
        EnemyTarget targetScript = demon.GetComponent<EnemyTarget>();
        targetScript.SetTarget(SacredTarget);
    }

    GameObject SpawnDemon(Vector3 location)
    {
       GameObject demon = Instantiate(demon1, location, Quaternion.identity);
        return demon;
    }
}
