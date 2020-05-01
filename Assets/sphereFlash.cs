using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereFlash : MonoBehaviour
{

    public GameObject sphere;

    public float sphereFlashTimerBase = .5f;
    public float sphereFlashTimer;

    // Start is called before the first frame update
    void Start()
    {
        sphereFlashTimer = sphereFlashTimerBase;
        sphere.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (sphereFlashTimer > 0)
        {
            sphereFlashTimer -= Time.deltaTime;
        }
        if (sphereFlashTimer < 0)
        {
            sphere.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (sphereFlashTimer < 0)
        {
            sphere.SetActive(true);
            sphereFlashTimer = sphereFlashTimerBase;
        }
    }
}
