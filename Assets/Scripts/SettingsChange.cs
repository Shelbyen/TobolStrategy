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
    public TMP_Text WaitingTime; //FirstWaitingTime
    public Button WaitingTimeMinus;
    public Button WaitingTimePlus;

    void Awake()
    {
        Sound.value = SettingsManager.SoundVolume;
        Music.value = SettingsManager.MusicVolume;
        Hints.isOn = SettingsManager.GameplayHints;
        Sensitivity.value = SettingsManager.Sensitivity;

        WaitingTime.text = $"{SettingsManager.FirstWaitingTime}";
        if (SettingsManager.FirstWaitingTime >= 120) WaitingTimePlus.interactable = false;
        else WaitingTimePlus.interactable = true;
        if (SettingsManager.FirstWaitingTime <= 60) WaitingTimeMinus.interactable = false;
        else WaitingTimeMinus.interactable = true;
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

    public void AddTime(int Time)
    {
        SettingsManager.FirstWaitingTime += Time;
        WaitingTime.text = $"{SettingsManager.FirstWaitingTime}";
        if (SettingsManager.FirstWaitingTime >= 120) WaitingTimePlus.interactable = false;
        else WaitingTimePlus.interactable = true;
        if (SettingsManager.FirstWaitingTime <= 60) WaitingTimeMinus.interactable = false;
        else WaitingTimeMinus.interactable = true;
    }
}
