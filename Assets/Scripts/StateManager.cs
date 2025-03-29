using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    public AudioSource Source;

    int Wave = 0;
    public Image timer;
    public float TimeBeforeRaid = 120;
    private float timeLeft;
    private bool isRaid = false;
    private bool startRaid = false;

    private bool ActiveStates;

    private Raid raidManager;

    void Awake()
    {
        raidManager = new Raid();

        TimeBeforeRaid = SettingsManager.FirstWaitingTime;
    }

    public void StartStates()
    {
        ActiveStates = true;
    }

    void FixedUpdate()
    {
        if (ActiveStates)
        {
            if (!isRaid) Timer();

            if (timer.fillAmount == 0 || startRaid)
            {
                startRaid = false;
                StartNewRaid();
            }
        }
    }

    public void Timer()
    {
        timeLeft += Time.fixedDeltaTime;
        timer.fillAmount = (TimeBeforeRaid - timeLeft) / TimeBeforeRaid;
        LinkManager.GetUIManager().ChangeTextRaidStatus(Localization.ReturnTranslation("calm"));
    }

    public void StartNewRaid()
    {
        isRaid = true;
        timeLeft = 0f;
        timer.fillAmount = 1;
        TimeBeforeRaid = 30;
        Wave += 1;

        raidManager.StartRaid(Wave);
        LinkManager.GetUIManager().ChangeTextRaidStatus(Localization.ReturnTranslation("wave") + " " + Wave);
        Source.Play();
    }

    public bool getState () {
        return isRaid;
    }

    public void setState (bool state) {
        isRaid = state;
    }

    public void StartRaid ()
    {
        if (ActiveStates) startRaid = true;
    }
}
