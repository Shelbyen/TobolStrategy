using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorkplaceData", menuName = "BuildingData/Workplace", order = 1)]
public class WorkplaceData : ScriptableObject
{
    public int[] MaxWorkers;
}
