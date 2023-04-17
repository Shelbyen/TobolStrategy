using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    public Image timer;
    public float TimeBeforeRaid = 20;
    private float _timeLeft;
    private bool _isRaid = false;

    private Raid _raidManager;

    private void Start() {
        _raidManager = new Raid();
    }

    void FixedUpdate()
    {   
        if (!_isRaid) {
            _timeLeft += (Time.fixedDeltaTime / TimeBeforeRaid) * 5;
            timer.fillAmount = _timeLeft / 10;
        }
        
        if (_timeLeft >= TimeBeforeRaid) {
            _isRaid = true;
            _raidManager.StartRaid();
            _timeLeft = 0f;
            timer.fillAmount = 0;
        }
    }
    public bool getState () {
        return _isRaid;
    }

    public void setState (bool state) {
        _isRaid = state;
    }
}
