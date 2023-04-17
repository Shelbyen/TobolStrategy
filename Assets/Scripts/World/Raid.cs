using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Raid : MonoBehaviour
{
    int reward;
    public GameObject[] Humans;
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
            var enemys = new ArrayList();
            var peace_human = new ArrayList();

            foreach (GameObject human in Humans)
            {
                if (human.GetComponent<Human>().IsEnemy) {
                    enemys.Add(human);
                }
                else {
                    peace_human.Add(human);
                }
            }
            if (enemys.Count == 0) {
                Camera.main.gameObject.GetComponentInParent<StateManager>().setState(false);
                ResourceManager.GetInstance().addGold(200 + reward);
            }
            else if (peace_human.Count == 0){
                Debug.Log("ТЫ - ЛУЗЕР");
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
