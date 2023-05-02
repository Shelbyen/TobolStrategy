using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public int[] ResidentsCount;

    public void AddResidents(int Lv)
    {
        ResourceManager.GetInstance().addMaxHumans(ResidentsCount[Lv]);
    }

    public void DeleteResidents(int Lv)
    {
        ResourceManager.GetInstance().addMaxHumans(-ResidentsCount[Lv]);
    }
}
