using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SettingsChange : MonoBehaviour
{
    public Slider Sound;
    public Slider Music;
    public Toggle Hints;
    public Slider Sensitivity;
    public Button FWT1; //FirstWaitingTime
    public Button FWT2;
    public Button FWT3;

    void Awake()
    {
        Sound.value = SettingsManager.SoundVolume;
        Music.value = SettingsManager.MusicVolume;
        Hints.isOn = SettingsManager.GameplayHints;
        Sensitivity.value = SettingsManager.Sensitivity;

        switch (SettingsManager.FirstWaitingTime)
        {
            case 60:
                {
                    FWT1.interactable = false;
                    break;
                }
            case 90:
                {
                    FWT2.interactable = false;
                    break;
                }
            case 120:
                {
                    FWT3.interactable = false;
                    break;
                }
        }
    }

    public void SetSound()
    {
        SettingsManager.SoundVolume = Sound.value;
        Debug.Log(SettingsManager.SoundVolume);
    }

    public void SetMusic()
    {
        SettingsManager.MusicVolume = Music.value;
        Debug.Log(SettingsManager.MusicVolume);
    }

    public void SetHints()
    {
        SettingsManager.GameplayHints = Hints.isOn;
        Debug.Log(SettingsManager.GameplayHints);
    }

    public void SetSensitivity()
    {
        SettingsManager.Sensitivity = Sensitivity.value;
        Debug.Log(SettingsManager.Sensitivity);
    }

    public void SetFirstWaiting(int Time)
    {
        FWT1.interactable = true; 
        FWT2.interactable = true;
        FWT3.interactable = true;
        switch (Time)
        {
            case 60:
                {
                    FWT1.interactable = false;
                    break;
                }
            case 90:
                {
                    FWT2.interactable = false;
                    break;
                }
            case 120:
                {
                    FWT3.interactable = false;
                    break;
                }
        }
        SettingsManager.FirstWaitingTime = Time;
        Debug.Log(SettingsManager.FirstWaitingTime);
    }
}
