using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workplace : MonoBehaviour
{
    protected Building Building;
    public int[] MaxWorkers;
    public int WorkersCount;

    void Awake()
    {
        Building = GetComponent<Building>();
    }

    void OnDestroy()
    {
        ResourceManager.GetInstance().useHuman(-WorkersCount);
    }

    public void HireWorker()
    {
        if (WorkersCount < MaxWorkers[Building.Level])
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
