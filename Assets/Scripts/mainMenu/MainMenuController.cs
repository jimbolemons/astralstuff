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

    public void GoToScene()
    {
        Invoke("BigGoToScene",.5f);
        
    }
    public void BigGoToScene()
    {
        //levelLoader.TriggerAnimation();
        SceneManager.LoadScene(0);
        
    }
    public void Quit()
    {
         Debug.Log("I am Quiting the game");
         Invoke("BigQuit",.5f);
         
        Application.Quit();
       
    }
    public void BigQuit()
    {

    }
   
   

    
}
