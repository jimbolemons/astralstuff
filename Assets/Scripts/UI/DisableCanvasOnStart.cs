using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Simple script that makes the pause menu invisible on starting the scene
/// </summary>
public class DisableCanvasOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }
}
