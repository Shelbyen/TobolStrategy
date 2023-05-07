using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    public AudioSource Source;

    int i = 0;
    public Image timer;
    public float TimeBeforeRaid = 120;
    private float _timeLeft;
    private bool _isRaid = false;
    private bool _startRaid = false;

    private VolumeValue Music;
    private UIManagerScript UIManager;
    private Raid _raidManager;

    private void Awake() {
        Music = GameObject.Find("MusicPlayer").GetComponent<VolumeValue>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManagerScript>();
        UIManager.ChangeTextRaidStatus("Затишье");
        _raidManager = new Raid();
    }
    
    void Start()
    {
        TimeBeforeRaid = SettingsManager.FirstWaitingTime;
    }

    void FixedUpdate()
    {
        if (!_isRaid) {
            _timeLeft += Time.fixedDeltaTime;
            timer.fillAmount = (TimeBeforeRaid - _timeLeft) / TimeBeforeRaid;
            UIManager.ChangeTextRaidStatus("Затишье");
        }

        if (timer.fillAmount == 0 || _startRaid) {
            Debug.Log("WAVE");
            _timeLeft = 0f;
            timer.fillAmount = 1;
            _isRaid = true;
            if (i == 0)
                {
                    TimeBeforeRaid = 30;
                }
            
            i += 1;

            _raidManager.StartRaid(i);
            UIManager.ChangeTextRaidStatus("Волна " + i);
            Source.Play();
            _startRaid = false;
        }
    }
    public bool getState () {
        return _isRaid;
    }

    public void setState (bool state) {
        _isRaid = state;
    }

    public void StartRaid ()
    {
        _startRaid = true;
    }
}
