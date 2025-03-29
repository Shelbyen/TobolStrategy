using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "BuildingData/Building", order = 1)]
public class BuildingData : ScriptableObject
{
    public string Name;
    public Sprite BuildingTypeImage;
    public string Discription;
    public int[] UpgradeCost;
    public float TikTimer;
}
