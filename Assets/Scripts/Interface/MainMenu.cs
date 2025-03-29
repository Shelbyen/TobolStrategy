using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string GameSceneName;
    public int StartGold = 1000;

    void Awake()
    {
        SceneManagerScript.StartGold = StartGold;
    }

    public void StartGame()
    {
        SceneManagerScript.LoadSceneByName(GameSceneName);
    }
    public void ExitGame()
    {
        SceneManagerScript.Quit();
    }
}
