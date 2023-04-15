using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    public Light dirLight;
    public float timeSnow = 15f;
    private ParticleSystem _ps;
    private bool _isSnow = false;

    private void Start() {
        _ps = GetComponent<ParticleSystem>();
        StartCoroutine(Weather());
    }

    private void Update() {
        if(_isSnow && dirLight.intensity > 0.2f) {
            LightIntensity(-1);
        }
        else if(!_isSnow && dirLight.intensity < 0.5f) {
            LightIntensity(1);
        }
    }
    
    private void LightIntensity(int mult) {
        dirLight.intensity += 0.15f * Time.deltaTime * mult;
    }

    IEnumerator Weather() {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(timeSnow - 30f, timeSnow));
            

            if(_isSnow) {
                _ps.Stop();
            }
            else {
                _ps.Play();
            }

            _isSnow = !_isSnow;
        }
    }
}
