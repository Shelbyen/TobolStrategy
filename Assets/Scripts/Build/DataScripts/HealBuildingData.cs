using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealBuildingData", menuName = "BuildingData/Heal", order = 1)]
public class HealBuildingData : ScriptableObject
{
    public float[] HealPerWorker;
    public float[] MaxHeal;
}
