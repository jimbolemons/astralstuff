using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAttackFire : MonoBehaviour
{
    public Gun gun;

    public void Fire()
    {
        //print("firing demon gun");
        gun.Fire();
    }
}
