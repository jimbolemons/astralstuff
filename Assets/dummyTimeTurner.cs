using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyTimeTurner : MonoBehaviour
{
    public ControlSwap player1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   void OnCollisionEnter(Collision collision)
   {
       player1.slow = false;
       Debug.Log("wtf");

   }
   
    
}
