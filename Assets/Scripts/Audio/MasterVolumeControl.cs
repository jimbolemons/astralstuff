using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterVolumeControl : MonoBehaviour
{

    [SerializeField]
    private float num;
    private void Start()
    {
        Invoke("LateStart", 1);
        
    }

    void Update()
    {
        if (AudioListener.volume != num)
        {
            AudioListener.volume = num;
        }

    }
    public void OnVolumeChange(float vol)
    {
        num = vol;
        FindObjectOfType<AudioManager>().VolumeChange(vol);
    }
    public void LateStart()
    {
        num = FindObjectOfType<AudioManager>().volu;
        Debug.Log(num);
    }
    public void Mute()
    {
        if (num > 0)
        {
            num = 0;
        }
        else
        {
            num = 1;
        }
    }
}
