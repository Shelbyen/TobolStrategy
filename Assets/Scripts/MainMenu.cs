using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int StartGold = 1000;

    public void PlayGame () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        ResourceManager.GetInstance().setGold(StartGold);
    }
    public void ExitGame () {
        Debug.Log(SettingsManager.SoundVolume);
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
