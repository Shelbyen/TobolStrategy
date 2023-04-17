using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raid : MonoBehaviour
{
    public GameObject[] Humans;
    public void StartRaid()
    {
        GameObject[] Buildings = GameObject.FindGameObjectsWithTag("EnemyHouse");
        Debug.Log(Buildings.Length);

        foreach (GameObject build in Buildings)
        {
            Building components = build.GetComponent<Building>();
            if (components.IsEnemy) {
                components.StartCoroutine(components.SpawnUnits());
            }
        }
    }

    void Update()
    {
        if (Camera.main.gameObject.GetComponentInParent<StateManager>().getState()) {
            var enemys = new ArrayList();
            Humans = GameObject.FindGameObjectsWithTag("Humanoid");

            foreach (GameObject human in Humans)
            {
                if (human.GetComponent<Human>().IsEnemy) {
                    enemys.Add(human);
                }
            }
            if (enemys.Count == 0) {
                Camera.main.gameObject.GetComponentInParent<StateManager>().setState(false);
            }
        }
    }
}
