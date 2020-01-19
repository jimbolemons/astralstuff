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

    public static GameObject playerTransform;

    private static MasterStaticScript instance;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectsWithTag("Player")[0];
        print(playerTransform.transform.position);
    }

    private void Update()
    {
        
    }
}
