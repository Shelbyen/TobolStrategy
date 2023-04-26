using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBuilding : MonoBehaviour
{
    public int[] GoldMining;
    
    public void GoldMine(int Lv)
    {
        ResourceManager.GetInstance().addGold(GoldMining[Lv]);
    }
}
