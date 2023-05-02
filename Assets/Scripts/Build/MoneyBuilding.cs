using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBuilding : MonoBehaviour
{
    public int[] GoldMining;
    
    public void GoldMine(int Lv)
    {
        ResourceManager.GetInstance().addGold((ResourceManager.GetInstance().maxHumansCount() - ResourceManager.GetInstance().usedHumansCount()) * GoldMining[Lv]);
    }
}
