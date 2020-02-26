using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolumeControl : MonoBehaviour
{

    [SerializeField]
    private float num;
    Slider mSlider;
    bool isSlider = false;
    private void Start()        
    {
        if (FindObjectOfType<Slider>() != null)
        {
            mSlider = FindObjectOfType<Slider>();
            isSlider = true;
        }
        Invoke("LateStart", .01f);        
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
       // Debug.Log(num);
        if (isSlider)
        {
            mSlider.value = num;
        }
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
