using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public bool PauseGame;
    public GameObject pauseGameMenu;

    public GameObject DefeatScreen;
    public TMP_Text MessageText;

    void Update()
    {

        if (InputManager.GetKeyDown("Cancel") && !DefeatScreen.activeSelf) 
        {
            if (PauseGame) {
                Resume();
            } else {
                Pause();
            }
        }
    }
    public void Resume() 
    {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseGame = false;
    }

    public void Pause () 
    {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseGame = true;
    }

    public void RestartGame()
    {
        SceneManagerScript.ReloadScene();
    }

    public void LoadMenu() 
    {
        SceneManagerScript.LoadSceneByName("Menu");
    }
    
    public void ExitGame () 
    {
        SceneManagerScript.Quit();
    }

    public void OpenDefeatScreen(bool Status, string Message)
    {
        Time.timeScale = 0f;
        MessageText.text = Message;
        DefeatScreen.SetActive(Status);
    }
}
