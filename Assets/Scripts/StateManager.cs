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

    private UIManagerScript UIManager;
    private Raid _raidManager;

    private void Awake() {
        UIManager = GameObject.Find("UIManager").GetComponent<UIManagerScript>();
        UIManager.ChangeTextRaidStatus("Waiting");
        _raidManager = new Raid();
    }
    
    void Start()
    {
        TimeBeforeRaid = SettingsManager.FirstWaitingTime;
    }

    void FixedUpdate()
    {
        if (!_isRaid) {
            _timeLeft += (Time.fixedDeltaTime / TimeBeforeRaid) * 5;
            timer.fillAmount = _timeLeft / 5;
            UIManager.ChangeTextRaidStatus("Waiting");
        }

        if (timer.fillAmount == 1 || _startRaid) {
            Debug.Log("WAVE");
            _timeLeft = 0f;
            timer.fillAmount = 0;
            _isRaid = true;
            if (i == 0)
                {
                    TimeBeforeRaid = 30;
                }
            _raidManager.StartRaid();

            i += 1;
            UIManager.ChangeTextRaidStatus("Wave " + i);
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
