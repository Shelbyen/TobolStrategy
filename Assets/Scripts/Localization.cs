using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Localization
{
    private static Dictionary<string, string> Translate;
    
    static Localization()
    {
        SetTranslateDictionary(1); //Установить английский по умолчанию
    }

    public static void SetTranslateDictionary(int Language)
    { //Смена языка
        Translate = new Dictionary<string, string>();

        StreamReader reader = new StreamReader(Application.dataPath + "/Resources/LocalizationFile.txt");
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] TranslateList = line.Split(" %");
            Translate.Add(TranslateList[0], TranslateList[Language]);
        }

        Debug.Log("Translated to " + Translate["language"]);
    }

    public static string ReturnTranslation(string Key)
    {
        return Translate[Key];
    }

    public static void ShowTranslation(string Key)
    {
        Debug.Log(Translate[Key]);
    }
}