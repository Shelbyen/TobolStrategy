using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Workplace : Building
{
    public int[] MaxWorkers;
    public int WorkersCount;

    public override void OnDestroy()
    {
        base.OnDestroy();
        if (Built)
        {
            ResourceManager.GetInstance().useHuman(-WorkersCount);
        }
    }

    public void HireWorker()
    {
        if (WorkersCount < MaxWorkers[Level])
        {
            ResourceManager.GetInstance().useHuman(1);
            WorkersCount += 1;
        }
    }

    public void FireWorker()
    {
        if (WorkersCount > 0)
        {
            ResourceManager.GetInstance().useHuman(-1);
            WorkersCount -= 1;
        }
    }
}
