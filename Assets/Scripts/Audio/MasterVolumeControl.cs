using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterVolumeControl : MonoBehaviour
{
    
    [SerializeField]
    
   
    void Update()
    {
        AudioListener.volume = FindObjectOfType<AudioManager>().volu;

    }
    public void OnVolumeChange(float vol)
    {
        
        FindObjectOfType<AudioManager>().VolumeChange(vol);
    }
}
