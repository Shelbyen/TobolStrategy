using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Workplace : Building
{
    public WorkplaceData WorkplaceData;
    public int WorkersCount;

    protected override void OnDestroy()
    {
        base.OnDestroy();
        ResourceManager.GetInstance().useHuman(-WorkersCount);
    }

    public override void AddUnit()
    {
        base.AddUnit();
        ResourceManager.GetInstance().useHuman(1);
        WorkersCount += 1;
    }

    public override void DeleteUnit()
    {
        base.DeleteUnit();
        ResourceManager.GetInstance().useHuman(-1);
        WorkersCount -= 1;
    }

    public string ReturnWorkersCount()
    {
        return (WorkersCount + "/" + WorkplaceData.MaxWorkers[Level]);
    }

    public override void ShowUnits()
    {
        LinkManager.GetUIManager().MainStats.SetUnitsInfo("working", ReturnWorkersCount());
    }

    public override void CheckVacancies()
    {
        if (ResourceManager.GetInstance().checkHuman() && WorkersCount < WorkplaceData.MaxWorkers[Level] && Built)
        {
            LinkManager.GetUIManager().MainStats.HireUnits(true);
        }
        else LinkManager.GetUIManager().MainStats.HireUnits(false);

        if (WorkersCount > 0 && Built)
        {
            LinkManager.GetUIManager().MainStats.FireUnits(true);
        }
        else LinkManager.GetUIManager().MainStats.FireUnits(false);

        LinkManager.GetUIManager().MainStats.SetUnitCost(0);
    }
}
