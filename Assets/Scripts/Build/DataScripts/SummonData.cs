using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SummonData", menuName = "BuildingData/Summon", order = 1)]
public class SummonData : ScriptableObject
{
    public GameObject Unit;
    public string UnitName;
    public int[] MaxUnitNumber;
    public int[] UnitCost;
    public float[] UnitDamage;
    public float[] UnitMaxHP;
    public float[] UnitSpeed;
}
