using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Only with Workplace class
public class HealBuilding : Workplace
{
    public float[] HealPerWorker;
    public float[] MaxHeal;

    void Awake()
    {
        Building = GetComponent<Building>();
    }

    public void Heal()
    {
        GameObject[] HumansForHeal = GameObject.FindGameObjectsWithTag("Human");
        foreach (GameObject human in HumansForHeal)
        {
            if (human.GetComponent<Human>().HP < human.GetComponent<Human>().MaxHP * MaxHeal[Building.Level])
            {
                human.GetComponent<Human>().HP += HealPerWorker[Building.Level] * WorkersCount;
                human.GetComponent<Human>().HP = Mathf.Clamp(human.GetComponent<Human>().HP, 0, human.GetComponent<Human>().MaxHP * MaxHeal[Building.Level]);
            }
        }
        Debug.Log(HealPerWorker[Building.Level] * WorkersCount);
    }

    public float ReturnHealPower()
    {
        return HealPerWorker[Building.Level] * WorkersCount;
    }
}
