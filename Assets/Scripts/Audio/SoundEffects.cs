using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffect: MonoBehaviour
{
    public AudioSource startfart;
    public AudioSource fart;
    // Start is called before the first frame update
    
        void Start()
        {

           fart.loop = false;
            
        }

    // Update is called once per frame


    
    public void PlayFart()
    {
        fart.Play();
       

    }
}
