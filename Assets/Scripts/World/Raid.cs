using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class Raid : MonoBehaviour
{
    int reward;
    public GameObject[] Humans;
    public GameObject[] Enemys;
    public void StartRaid()
    {
        reward = 0;
        GameObject[] Buildings = GameObject.FindGameObjectsWithTag("EnemyHouse");
        Debug.Log(Buildings.Length);

        foreach (GameObject build in Buildings)
        {
            Building components = build.GetComponent<Building>();
            if (components.IsEnemy) {
                components.StartCoroutine(components.SpawnUnits());
                reward += components.UnitNumber * 10;
                Debug.Log(components.UnitNumber);
            }
        }
    }

    void Update()
    {
        if (Camera.main.gameObject.GetComponentInParent<StateManager>().getState()) {
            Humans = GameObject.FindGameObjectsWithTag("Human");
            Enemys = GameObject.FindGameObjectsWithTag("Enemy");

            if (Enemys.Length == 0) {
                Camera.main.gameObject.GetComponentInParent<StateManager>().setState(false);
                ResourceManager.GetInstance().addGold(200 + reward);
            }
            else if (Humans.Length == 0){
                Debug.Log("ТЫ - ЛУЗЕР");
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
