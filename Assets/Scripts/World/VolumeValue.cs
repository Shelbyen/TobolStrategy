using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeValue : MonoBehaviour
{
    private AudioSource audioSrc;
    public AudioClip[] MusicList_Calm;
    public AudioClip[] MusicList_Raid;
    public AudioClip MenuMusic;
    public AudioClip DefeatMusic;

    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSrc.volume = SettingsManager.MusicVolume;
    }
}
