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
            _timeLeft += (Time.fixedDeltaTime / TimeBeforeRaid) * 5;
            Debug.Log(_timeLeft);
            timer.fillAmount = _timeLeft / 5;
        }
        
        if (_timeLeft >= TimeBeforeRaid) {
            _isRaid = true;
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
