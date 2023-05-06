using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool PauseGame;
    public GameObject pauseGameMenu;
    public GameObject[] ConflictWindows;

    void Update()
    {
        bool CanPause = true;
        foreach (GameObject Window in ConflictWindows)
        {
            if (Window.activeSelf) CanPause = false;
        }

        if (InputManager.GetKeyDown("Cancel") && CanPause) {
            if (PauseGame) {
                Resume();
            } else {
                Pause();
            }
        }
    }
    public void Resume() {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseGame = false;
    }

    public void Pause () {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseGame = true;
    }

    public void LoadMenu () {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    
    public void ExitGame () {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
