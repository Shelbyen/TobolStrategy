using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Only with Workplace class
public class HealBuilding : Workplace
{
    public float[] HealPerWorker;
    public float[] MaxHeal;

    public override void NewTik()
    {
        base.NewTik();
        HealUnits();
    }

    public void HealUnits()
    {
        GameObject[] HumansForHeal = GameObject.FindGameObjectsWithTag("Human");
        foreach (GameObject human in HumansForHeal)
        {
            if (human.GetComponent<Human>().HP < human.GetComponent<Human>().MaxHP * MaxHeal[Level])
            {
                human.GetComponent<Human>().HP += HealPerWorker[Level] * WorkersCount;
                human.GetComponent<Human>().HP = Mathf.Clamp(human.GetComponent<Human>().HP, 0, human.GetComponent<Human>().MaxHP * MaxHeal[Level]);
            }
        }
        Debug.Log(HealPerWorker[Level] * WorkersCount);
    }

    public float ReturnHealPower()
    {
        return HealPerWorker[Level] * WorkersCount;
    }
}
