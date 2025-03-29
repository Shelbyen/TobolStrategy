using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System.Linq;

public class SummonBuilding : Building
{
    public SummonData SummonData;
    public GameObject Enter; //Вектор3
    public List<GameObject> BuildingUnits;

    protected void FixedUpdate()
    {
        Clear();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        KillAll();
    }

    public override void UpgradeThis()
    {
        base.UpgradeThis();
        UpgradeUnits();
    }

    public void UpgradeUnits()
    {
        foreach (GameObject Unit in BuildingUnits)
        {
            Unit.GetComponent<Human>().MaxHP = SummonData.UnitMaxHP[Level];
            Unit.GetComponent<Human>().meleeDamage = SummonData.UnitDamage[Level];
            Unit.GetComponent<NavMeshAgent>().speed = SummonData.UnitSpeed[Level];
        }
    }

    public override void AddUnit()
    {
        base.AddUnit();
        ResourceManager.GetInstance().useHuman(1);
        ResourceManager.GetInstance().checkAndBuyGold(SummonData.UnitCost[Level]);
        BuildingUnits.Add(Instantiate(SummonData.Unit, Enter.transform.position, Quaternion.identity));
        BuildingUnits.Last().GetComponent<Human>().Summon = GetComponent<SummonBuilding>();
        BuildingUnits.Last().GetComponent<Human>().UpdateLevelData(Level);
        BuildingUnits.Last().GetComponent<Human>().HP = SummonData.UnitMaxHP[Level];
    }

    public override void DeleteUnit()
    {
        base.DeleteUnit();
        Destroy(BuildingUnits.Last());
        BuildingUnits.RemoveAt(BuildingUnits.Count - 1);
    }

    public void Clear()
    {
        for (int i = 0; i < BuildingUnits.Count; i += 1)
        {
            if (BuildingUnits[i] == null)
            {
                BuildingUnits.RemoveAt(i);
            }
        }
    }

    public bool CheckUnits()
    {
        return SummonData.MaxUnitNumber[Level] > BuildingUnits.Count;
    }

    public void KillAll()
    {
        foreach (GameObject UnitForKill in BuildingUnits)
        {
            Destroy(UnitForKill);
        }
    }

    public string ReturnUnitsCount()
    {
        return (BuildingUnits.Count + "/" + SummonData.MaxUnitNumber[Level]);
    }

    public override void ShowStats()
    {
        base.ShowStats();
        LinkManager.GetUIManager().SummonStats.SetName(SummonData.UnitName);
        LinkManager.GetUIManager().SummonStats.SetDamage(SummonData.UnitDamage[Level]);
        LinkManager.GetUIManager().SummonStats.SetSpeed(SummonData.UnitSpeed[Level]);
        LinkManager.GetUIManager().SummonStats.SetHealth(SummonData.UnitMaxHP[Level]);

        LinkManager.GetUIManager().SummonStats.SetWindowStatus(true);
    }

    public override void ShowUnits()
    {
        LinkManager.GetUIManager().MainStats.SetUnitsInfo("fighting", ReturnUnitsCount());
    }

    public override void CheckVacancies()
    {
        if (Built && ResourceManager.GetInstance().checkGold(SummonData.UnitCost[Level]) && ResourceManager.GetInstance().checkHuman() && CheckUnits())
        {
            LinkManager.GetUIManager().MainStats.HireUnits(true);
        }
        else LinkManager.GetUIManager().MainStats.HireUnits(false);

        if (Built && BuildingUnits.Count > 0)
        {
            LinkManager.GetUIManager().MainStats.FireUnits(true);
        }
        else LinkManager.GetUIManager().MainStats.FireUnits(false);

        LinkManager.GetUIManager().MainStats.SetUnitCost(SummonData.UnitCost[Level]);
    }
}
