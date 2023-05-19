using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoldMiningBuilding : Workplace
{
    public int[] MoneyPerWorker;

    public override void NewTik()
    {
        base.NewTik();
        MineGold();
    }

    public void MineGold()
    {
        ResourceManager.GetInstance().addGold(ReturnGoldMining());
    }

    public int ReturnGoldMining()
    {
        return MoneyPerWorker[Level] * WorkersCount;
    }
}