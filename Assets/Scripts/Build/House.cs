using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building
{
    public int[] ResidentsCount;

    public override void OnDestroy()
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

        public void AddResidents(int Lv)
    {
        ResourceManager.GetInstance().addMaxHumans(ResidentsCount[Lv]);
    }

    public void DeleteResidents(int Lv)
    {
        ResourceManager.GetInstance().addMaxHumans(-ResidentsCount[Lv]);
    }
}
