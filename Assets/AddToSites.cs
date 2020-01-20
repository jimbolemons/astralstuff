using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToSites : MonoBehaviour
{
    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        MasterStaticScript.sacredSites.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead) killSite();
        else
        {
            //do your thing.
        }
    }

    void killSite()
    {
        print("Site is dead.");
        MasterStaticScript.RemoveFromObjectList(gameObject, MasterStaticScript.sacredSites);
        MasterStaticScript.CheckForGameLose();
    }
}
