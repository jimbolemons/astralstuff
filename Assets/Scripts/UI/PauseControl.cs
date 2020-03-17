using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// handles the pause canvas and pause function
/// </summary>
public class PauseControl : MonoBehaviour
{
    public MainMenuController pauseMenu;
    bool prevPaused = false;
    [Tooltip("Reference to the pause canvas.")]
    public Canvas pauseCanvas;
    

    private void Start()
    {
        pauseCanvas.enabled = false;
    }

    void Update()
    {
        //TODO: Refactor this so it correctly uses the functions

        //if the player presses pause, tell the master script to pause the game
        if (Input.GetAxis("Pause") != 0 && prevPaused == MasterStaticScript.gameIsPaused)        
        {
            MasterStaticScript.gameIsPaused = !MasterStaticScript.gameIsPaused;
            SetPauseCanvasState();

            print("pause state - " + MasterStaticScript.gameIsPaused);
        }

        //won't reset pause check until button is released
        if (Input.GetAxis("Pause") == 0)
        {
            prevPaused = MasterStaticScript.gameIsPaused;

        }
    }
     
   /// <summary>
   /// Public function to pause the game
   /// </summary>
    public void Pause()
    {
       
        MasterStaticScript.gameIsPaused = true;
        SetPauseCanvasState();
       
    
}
    /// <summary>
    /// Public function to unpause game
    /// </summary>
    public void UnPause()
    {
        
        MasterStaticScript.gameIsPaused = false;
        SetPauseCanvasState();
        
    
}
    /// <summary>
    /// function that sets the master script to the correct state
    /// </summary>
    void SetPauseCanvasState()
    {
        pauseCanvas.enabled = MasterStaticScript.gameIsPaused;
    }
}
