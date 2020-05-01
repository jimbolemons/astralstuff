using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class containing static variables for easy reference in the scene
/// </summary>
public class MasterStaticScript : MonoBehaviour
{

    [Tooltip("Game Paused State.")]
    public static bool gameIsPaused = false;

    public static GameObject playerReference;

    public static Camera mainCameraReference;

    private static MasterStaticScript instance;

    public static LevelTransitionLoader LevelLoader;

    [Tooltip("Stores all the Sacred Sites in the scene.")]
    public static List<GameObject> sacredSites = new List<GameObject>();
    [Tooltip("Stores all the Enemy Gates in the scene.")]
    public static List<GameObject> enemyGates = new List<GameObject>();
    [Tooltip("Stores all references to enemies in the scene.")]
    public static List<GameObject> enemyList = new List<GameObject>();


    void Awake()
    {
        playerReference = GameObject.FindGameObjectsWithTag("Player")[0];
       // Debug.Log(playerReference);
       // print(playerReference.transform.position);
    }

    private void Update()
    {
        
    }

    public static void RemoveFromObjectList(GameObject objectToRemove, List<GameObject> objectList)
    {
        objectList.Remove(objectToRemove);
        //print("Current number of objects in List: " + objectList.Count);
    }

    public static void CheckForGameLose()
    {
        if (sacredSites.Count <= 0)
        {
            print("Game has been lost. Via Site Distruction");
            LevelLoader.LoadScene("loseMk2");
            //TODO: change Game's lose state to true, go to lose scene.
        }       
    }

    public static void PlayerDead()
    {
        print("Game has been lost.");
        try{
            sacredSites.Clear();
        LevelLoader.LoadScene("loseMk2");
        }
        catch
        {            print("WARNING!! May need to check which lose state is loaded in build settings");
        LevelLoader.LoadScene("loseState");
        }
    }

    public static void CheckForGameWin()
    {
        if (enemyGates.Count <= 0)
        {
        print("Game has been won!");

            LevelLoader.LoadScene("WinState");
        }
    }    
}
