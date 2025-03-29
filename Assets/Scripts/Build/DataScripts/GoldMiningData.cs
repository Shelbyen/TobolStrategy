using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GoldMiningData", menuName = "BuildingData/GoldMining", order = 1)]
public class GoldMiningData : ScriptableObject
{
    public int[] MoneyPerWorker;
}
