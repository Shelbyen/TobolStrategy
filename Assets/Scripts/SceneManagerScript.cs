using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class SceneManagerScript
{
    public static int StartGold = 1000;

    public static void LoadSceneByName(string Name)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Name);
    }

    public static void LoadSceneByNumber(int Number)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Number);
    }

    public static void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void SetResource()
    {
        ResourceManager.GetInstance().setGold(StartGold);
    }

    public static void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
