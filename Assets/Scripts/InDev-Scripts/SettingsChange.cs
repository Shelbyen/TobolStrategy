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
    public Button WT1; //WaitingTime
    public Button WT2;
    public Button WT3;

    void Awake()
    {
        Sound.value = SettingsManager.SoundVolume;
        Music.value = SettingsManager.MusicVolume;
        Hints.isOn = SettingsManager.GameplayHints;
        Sensitivity.value = SettingsManager.Sensitivity;
    }

    public void SetSound()
    {
        SettingsManager.SoundVolume = (int)Mathf.Round(Sound.value * 100);
        Debug.Log(SettingsManager.SoundVolume);
    }

    public void SetMusic()
    {
        SettingsManager.MusicVolume = (int)Mathf.Round(Music.value * 100);
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
}
