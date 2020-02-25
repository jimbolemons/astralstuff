using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    KeyCode quitKey = KeyCode.P;

    [SerializeField]
    KeyCode pauseKey = KeyCode.Escape;
    bool gamePaused = false;

    public GameObject setiings;
    public GameObject menu;

    private void Update()
    {
        if (Input.GetKeyDown(quitKey))
            Quit();

        if (Input.GetKeyDown(pauseKey))
        {
            if (!gamePaused)
            {
                PauseGame();
            }
            else
            {
                UnPauseGame();
            }
        }            
    }

    public void GoToScene(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("I am Quiting the game");
    }
    public void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0;
    }
    public void UnPauseGame()
    {
        gamePaused = false;
        Time.timeScale = 1;
    }
    public void Mute()
    {

    }

    
}
