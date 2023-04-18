using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    int i = 0;
    public Image timer;
    public float TimeBeforeRaid = 120;
    private float _timeLeft;
    private bool _isRaid = false;

    private UIManagerScript UIManager;
    private Raid _raidManager;

    private void Awake() {
        UIManager = GameObject.Find("UIManager").GetComponent<UIManagerScript>();
        UIManager.ChangeTextRaidStatus("Waiting");
        _raidManager = new Raid();
    }

    void FixedUpdate()
    {   
        if (!_isRaid) {
            _timeLeft += (Time.fixedDeltaTime / TimeBeforeRaid) * 5;
            timer.fillAmount = _timeLeft / 5;
            UIManager.ChangeTextRaidStatus("Waiting");
        }

        if (timer.fillAmount == 1) {
            _isRaid = true;
            if (i == 0)
                {
                    TimeBeforeRaid = 30;
                }
            _raidManager.StartRaid();

            _timeLeft = 0f;
            timer.fillAmount = 0;
            i += 1;
            UIManager.ChangeTextRaidStatus("Wave " + i);
        }
    }
    public bool getState () {
        return _isRaid;
    }

    public void setState (bool state) {
        _isRaid = state;
    }
}
