using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    void Awake()
    {
        SingletonCheck();
        
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
    }
    void SingletonCheck()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

    }
}
