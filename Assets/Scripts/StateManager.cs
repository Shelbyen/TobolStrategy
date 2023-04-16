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

    void FixedUpdate()
    {   
        if (!_isRaid) {
            _timeLeft += (Time.fixedDeltaTime / TimeBeforeRaid) * 10;
            timer.fillAmount = _timeLeft / 10;
        }
        
        if (_timeLeft >= TimeBeforeRaid) {
            _isRaid = true;
            _timeLeft = 0f;
            timer.fillAmount = 0;
        }
    }
    bool getState () {
        return _isRaid;
    }

    public void setState (bool state) {
        _isRaid = state;
    }
}
