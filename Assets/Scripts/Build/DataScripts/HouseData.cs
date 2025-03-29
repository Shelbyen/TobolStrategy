using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HouseData", menuName = "BuildingData/House", order = 1)]
public class HouseData : ScriptableObject
{
    public int[] ResidentsCount;
}
