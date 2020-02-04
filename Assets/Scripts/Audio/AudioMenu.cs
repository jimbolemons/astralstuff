using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioMenu : MonoBehaviour
{
    public AudioClip startClip;
    public AudioClip loopClip;

    public AudioSource start;
    public AudioSource loop;
    void Start()
    {
        loop.loop = true;
        StartCoroutine(playSound());
    }

    IEnumerator playSound()
    {        
        start.Play();
        yield return new WaitForSeconds(start.clip.length);       
        loop.Play();
    }
}


