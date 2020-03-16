using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseParticles : MonoBehaviour
{
    ParticleSystem partical;
    // Start is called before the first frame update
    void Start()
    {
        partical = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!MasterStaticScript.gameIsPaused)
        {
            partical.Play();
        }
        else
        {
            partical.Pause();
        }
    }
}
