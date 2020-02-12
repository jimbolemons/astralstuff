using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class containing static variables for easy reference in the scene
/// </summary>
public class MasterStaticScript : MonoBehaviour
{

    [Tooltip("Game Paused State.")]
    public static bool gameIsPaused = false;

    public static GameObject playerTransform;

    private static MasterStaticScript instance;

    [Tooltip("Stores all the Sacred Sites in the scene.")]
    public static List<GameObject> sacredSites = new List<GameObject>();
    [Tooltip("Stores all the Enemy Gates in the scene.")]
    public static List<GameObject> enemyGates = new List<GameObject>();
    [Tooltip("Stores all references to enemies in the scene.")]
    public static List<GameObject> enemyList = new List<GameObject>(); 

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectsWithTag("Player")[0];
        print(playerTransform.transform.position);
    }

    private void Update()
    {
        
    }

    public static void RemoveFromObjectList(GameObject objectToRemove, List<GameObject> objectList)
    {
        objectList.Remove(objectToRemove);
        print("Current number of objects in List: " + objectList.Count);
    }

    public static void CheckForGameLose()
    {
        if (sacredSites.Count <= 0)
        {
            print("Game has been lost. Via Site Distruction");
            SceneManager.LoadScene("LoseState");
            //TODO: change Game's lose state to true, go to lose scene.

        }       
    }

    public static void PlayerDead()
    {
        print("Game has been lost.");
        SceneManager.LoadScene("LoseState");
    }

    public static void CheckForGameWin()
    {
        if (enemyGates.Count <= 0)
        {
        print("Game has been won!");
            SceneManager.LoadScene("WinState");
        //TODO: change Game's win state to true, go to win scene.
        }
    }
}
