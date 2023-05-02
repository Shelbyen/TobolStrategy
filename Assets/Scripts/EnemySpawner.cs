using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    public Vector3 SpawnPos;
    public GameObject EnemyUnit;
    public int BaseEnemyCount;
    public int EnemyPerWave;

    public IEnumerator SpawnEnemys(int Wave)
    {
        foreach (int i in Enumerable.Range(0, BaseEnemyCount + Wave * EnemyPerWave))
        {
            GameObject Unit = Instantiate(EnemyUnit, SpawnPos, Quaternion.identity);
            Debug.Log(Unit.transform.position);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
