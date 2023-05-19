using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Raid : MonoBehaviour
{
    int reward;
    public GameObject[] Humans;
    public GameObject[] Enemys;

    private UIManagerScript UIManager;
    private PauseMenu Pause;
    private string Message = "";
    void Awake()
    {
        UIManager = GameObject.Find("UIManager").GetComponent<UIManagerScript>();
        Pause = GameObject.Find("MainInterface - Canvas").GetComponent<PauseMenu>();
    }

    public void StartRaid(int Wave)
    {
        reward = 0;
        GameObject[] Buildings = GameObject.FindGameObjectsWithTag("EnemyHouse");
        Debug.Log(Buildings.Length);

        foreach (GameObject build in Buildings)
        {
            EnemySpawner Summon = build.GetComponent<EnemySpawner>();
            Summon.StartCoroutine(Summon.SpawnEnemys(Wave));
            reward += Summon.BaseEnemyCount * 10 + Summon.EnemyPerWave * Wave * 10;
            Debug.Log(Summon.BaseEnemyCount);
        }
    }

    void Update()
    {

        if (Camera.main.gameObject.GetComponentInParent<StateManager>().getState())
        {
            Humans = GameObject.FindGameObjectsWithTag("Human");
            Enemys = GameObject.FindGameObjectsWithTag("Enemy");

            if (Enemys.Length == 0)
            {
                Camera.main.gameObject.GetComponentInParent<StateManager>().setState(false);
                ResourceManager.GetInstance().addGold(200 + reward);
            }
            else if (Humans.Length == 0)
            {
                Camera.main.gameObject.GetComponentInParent<StateManager>().setState(false);
                Debug.Log("ТЫ - ЛУЗЕР");
                Message = "Защитники крепости пали";
                Pause.OpenDefeatScreen(true, Message);
            }
        }
    }
}
