using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearSitelist : MonoBehaviour
{
    // Start is called before the first frame update

    void Awake()
    {
        MasterStaticScript.enemyGates.Clear();
        //Debug.Log(MasterStaticScript.enemyGates.Count);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
