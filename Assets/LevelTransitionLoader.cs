using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionLoader : MonoBehaviour
{
    public Animator crossfade;
    public float fadeDuration = 1;
    AudioManager sounds;
    // Start is called before the first frame update
    void Awake()
    {
        MasterStaticScript.LevelLoader = this; 
        sounds = FindObjectOfType<AudioManager>();       
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(LoadLevel(scene));
    }
    public void LoadScene(int SceneNumber)
    {
        //levelLoader.TriggerAnimation();
       StartCoroutine(LoadLevel(SceneNumber));
    }
    public void TriggerAnimation()
    {
        crossfade.SetTrigger("Start");
    }
    IEnumerator LoadLevel(string level)
    {
        sounds.StopAll();
        TriggerAnimation();
        yield return new WaitForSeconds(fadeDuration);

        SceneManager.LoadScene(level);
    }
    IEnumerator LoadLevel(int level)
    {
        sounds.StopAll();
        TriggerAnimation();
        yield return new WaitForSeconds(fadeDuration);

        SceneManager.LoadScene(level);
    }
    public void Quit()
    {
        TriggerAnimation();
        Application.Quit();
        Debug.Log("I am Quiting the game");
    }
}
