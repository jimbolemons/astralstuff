using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this is a utility script that just causes the selected gun to continuously fire
/// </summary>
public class AutoFire : MonoBehaviour
{
    public Gun gun;

    // Update is called once per frame
    void Update()
    {
        if (!MasterStaticScript.gameIsPaused)
        {
            gun.Fire();
        }
    }
}
