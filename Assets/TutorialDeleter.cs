using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDeleter : MonoBehaviour
{

    public List<GameObject> objectsToDelete = new List<GameObject>();


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            foreach (GameObject g in objectsToDelete)
            {
                Destroy(g);
            }
            Destroy(gameObject);
        }
    }
}
