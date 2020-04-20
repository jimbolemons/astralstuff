using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    //[SerializeField]
    //KeyCode quitKey = KeyCode.P;

    [SerializeField]
    KeyCode pauseKey = KeyCode.Escape;
    bool gamePaused = false;

    public GameObject setiings;
    public GameObject menu;
    private float volumeFloat = 1f;
    private AudioSource audioSrc;

    public LevelTransitionLoader levelLoader;

    private void Start()
    {
        
        
    }

    private void Update()
    {
           
    }

    public void GoToScene(int SceneNumber)
    {
        //levelLoader.TriggerAnimation();
        SceneManager.LoadScene(SceneNumber);
        
    }
    public void Quit()
    {
         Debug.Log("I am Quiting the game");
        Application.Quit();
       
    }
   
   

    
}
