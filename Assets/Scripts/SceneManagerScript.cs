using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class SceneManagerScript
{
    public static int StartGold = 1000;

    public static void LoadSceneByName(string Name)
    {
        SceneManager.LoadScene(Name);
        Time.timeScale = 1f;
    }

    public static void LoadSceneByNumber(int Number)
    {
        SceneManager.LoadScene(Number);
        Time.timeScale = 1f;
    }

    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
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
