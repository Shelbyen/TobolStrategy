using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Church : Building
{
    [SerializeField] protected ChurchData ChurchData;

    protected override void NewTik()
    {
        base.NewTik();
        AddFaith();
    }

    protected void AddFaith()
    {
        ResourceManager.GetInstance().addFaith(PredictFaith());
    }

    public int GetHumans()
    {
        return ResourceManager.GetInstance().maxHumansCount() - ResourceManager.GetInstance().usedHumansCount();
    }

    public int PredictFaith()
    {
        return (GetHumans() * ChurchData.FaithPerHuman[Level]);
    }

    public override void ShowUnits()
    {
        LinkManager.GetUIManager().MainStats.SetUnitsInfo("pray", GetHumans().ToString());
    }
}
