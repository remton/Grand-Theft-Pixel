using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggle : MonoBehaviour {

    public Toggle FullscreenToggleUI;



    private void Start()
    {
       if(PlayerPrefs.GetInt("FullScreenEnabled") == 1)
        {
            Screen.fullScreen = true;
            FullscreenToggleUI.isOn = true;
        }
        else
        {
            Screen.fullScreen = false;
            FullscreenToggleUI.isOn = false;
        }
    }

    public void SetFullscreen(bool isFullScreen)
    {
        if(isFullScreen == true)
        {
            PlayerPrefs.SetInt("FullScreenEnabled", 1);
            Screen.fullScreen = true;
        }
        else
        {
            PlayerPrefs.SetInt("FullScreenEnabled", 0);
            Screen.fullScreen = false;
        }

    }
}
