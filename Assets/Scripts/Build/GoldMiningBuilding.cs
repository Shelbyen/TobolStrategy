using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoldMiningBuilding : Workplace
{
    [SerializeField] protected GoldMiningData GoldMiningData;

    protected override void NewTik()
    {
        base.NewTik();
        MineGold();
    }

    protected void MineGold()
    {
        ResourceManager.GetInstance().addGold(PredictGoldMining());
    }

    public int PredictGoldMining()
    {
        return GoldMiningData.MoneyPerWorker[Level] * WorkersCount;
    }

    public int MoneyPerWorker()
    {
        return GoldMiningData.MoneyPerWorker[Level];
    }

    public override void ShowStats()
    {
        base.ShowStats();
        LinkManager.GetUIManager().MiningStats.SetMining(PredictGoldMining());
        LinkManager.GetUIManager().MiningStats.SetWindowStatus(true);
    }
}