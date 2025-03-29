using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Only with Workplace class
public class HealBuilding : Workplace
{
    [SerializeField] protected HealBuildingData HealBuildingData;

    protected override void NewTik()
    {
        base.NewTik();
        HealUnits();
    }

    protected void HealUnits()
    {
        GameObject[] HumansForHeal = GameObject.FindGameObjectsWithTag("Human");
        foreach (GameObject human in HumansForHeal)
        {
            human.GetComponent<Human>().HP += ReturnHealPower();
            human.GetComponent<Human>().HP = Mathf.Clamp(human.GetComponent<Human>().HP, 0, human.GetComponent<Human>().MaxHP * HealBuildingData.MaxHeal[Level]);
        }
        Debug.Log(HealBuildingData.HealPerWorker[Level] * WorkersCount);
    }

    public float ReturnHealPower()
    {
        return HealBuildingData.HealPerWorker[Level] * WorkersCount;
    }

    public override void ShowStats()
    {
        base.ShowStats();
        LinkManager.GetUIManager().HealStats.SetHealPerTik(ReturnHealPower());
        LinkManager.GetUIManager().HealStats.SetMaxHealth(HealBuildingData.MaxHeal[Level]);

        LinkManager.GetUIManager().HealStats.SetWindowStatus(true);
    }
}
