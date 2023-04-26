using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBuilding : MonoBehaviour
{
    public GameObject Unit;
    public int UnitNumber;
    public GameObject Enter; //В Вектор3

    private GameObject[] BuildingsUnits;

    void Awake()
    {
        BuildingsUnits = new GameObject[UnitNumber];
    }

    void OnDestroy()
    {
        KillAll();
    }

    public IEnumerator RespawnUnits()
    {
        for (int i = 0; i < UnitNumber; i += 1)
        {
            if (BuildingsUnits[i] == null)
            {
                BuildingsUnits[i] = Instantiate(Unit);
                BuildingsUnits[i].transform.position = Enter.transform.position;
                BuildingsUnits[i].GetComponent<Human>().Target = Enter.transform.position;
                /*if (IsEnemy)
                {
                    BuildingsUnits[i].tag = "Enemy";
                }
                else
                {
                    BuildingsUnits[i].tag = "Human";
                }*/
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    public void KillAll()
    {
        foreach (GameObject UnitForKill in BuildingsUnits) Destroy(UnitForKill);
    }
}
