using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CenterMouse : MonoBehaviour
{
    // Start is called before the first frame update
    Scene m_Scene;
    string sceneName;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneName == "TutorialGreyBox" && !MasterStaticScript.gameIsPaused || sceneName == "Sandbox_Kyle 1" && !MasterStaticScript.gameIsPaused || sceneName == "josh_tutorialarea" && !MasterStaticScript.gameIsPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }
}
