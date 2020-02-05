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
        if (sceneName == "GateCrashing")
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        
    }
}
