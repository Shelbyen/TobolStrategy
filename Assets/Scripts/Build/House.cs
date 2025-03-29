using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building
{
    [SerializeField] protected HouseData HouseData;

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (Built)
        {
            DeleteResidents(Level);
        }
    }

    public override void BuildThis()
    {
        base.BuildThis();
        AddResidents(Level);
    }

    public override void UpgradeThis()
    {
        base.UpgradeThis();
        AddResidents(Level);
        DeleteResidents(Level - 1);
    }

    protected void AddResidents(int Lv)
    {
        ResourceManager.GetInstance().addMaxHumans(HouseData.ResidentsCount[Lv]);
    }

    protected void DeleteResidents(int Lv)
    {
        ResourceManager.GetInstance().addMaxHumans(-HouseData.ResidentsCount[Lv]);
    }

    public override void ShowUnits()
    {
        LinkManager.GetUIManager().MainStats.SetUnitsInfo("living", HouseData.ResidentsCount[Level].ToString());
    }
}
