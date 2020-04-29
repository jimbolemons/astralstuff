using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeResolution : MonoBehaviour
{
    //public Dropdown resolutionList;
    //public Dropdown screenModeList;
    public Dropdown resolutionList;
    public Dropdown screenModeList;
    

    public void ApplyChanges()
    {
        print(resolutionList.value);
        print(screenModeList.value);

        int resInt = resolutionList.value;
        int sModeInt = screenModeList.value;

        FullScreenMode screenMode = FullScreenMode.FullScreenWindow;
        switch (sModeInt)
        {
            case 1:
                screenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                screenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 3:
                screenMode = FullScreenMode.Windowed;
                break;
        }

        switch (resInt)
        {
            case 0:
                Screen.SetResolution(1920, 1080, screenMode);
                break;
            case 1:
                Screen.SetResolution(1600, 900, screenMode);
                break;
            case 2:
                Screen.SetResolution(1280, 720, screenMode);
                break;
            case 3:
                Screen.SetResolution(1024, 576, screenMode);
                break;
        }
    }
}
