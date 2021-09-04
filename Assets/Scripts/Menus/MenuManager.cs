using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour {

    //Audio Stuff
    public AudioMixer MainMixer;
    public float MainVolume = 0;
    private void Start()
    {
        MainVolume = PlayerPrefs.GetFloat("MVolume", 1f);
        MainMixer.SetFloat("Volume", PlayerPrefs.GetFloat("MVolume",1f));
    }

    public void SetVolume(float volume)
    {
        if (volume < -19)
            volume = -100;

        PlayerPrefs.SetFloat("MVolume", volume);
        MainMixer.SetFloat("Volume", volume);
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    /// <summary> Pass the build index of the scene for the level </summary>
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void EnableMenu(GameObject menu)
    {
        menu.SetActive(true);
    }
    public void DisableMenu(GameObject menu)
    {
        menu.SetActive(false);
    }




}
