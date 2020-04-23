using System;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : Singleton<AudioManager>
{
    public static AudioManager instance;
    public Sound[] sounds;

    public float volu = 1f;

   

    void Start()
    {

       

              
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Play("MLG");
           // Debug.Log("fuck you ya cunt");
        }
        if(MasterStaticScript.gameIsPaused)
        {
            foreach (Sound s in sounds)
            {

            s.source.Pause();

            }

        }
        else 
        {
             foreach (Sound s in sounds)
            {
                
                s.source.UnPause();
                

            }
        }
       
    }

    public void Play(string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log(name + " is spelled wrong");
            return;
        }
        s.source.Play();
       // s.playing = true;
        //Debug.Log("fuck you");
        
    }

     public void Stop(string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log(name + " is spelled wrong");
            return;
        }
        s.source.Stop();
        //s.playing = false;
        //Debug.Log("fuck you");
        
    }

    public void VolumeChange(float vol)
    {
        
        foreach (Sound s in sounds)
        {
            volu = vol;

        }
    }
    
}
