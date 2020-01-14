﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class containing static variables for easy reference in the scene
/// </summary>
public class MasterStaticScript : MonoBehaviour
{

    [Tooltip("Game Paused State.")]
    public static bool gameIsPaused = false;
    [Tooltip("Player's Current Position.")]
    public static Transform playerTransform = GameObject.FindGameObjectsWithTag("Player");
}
