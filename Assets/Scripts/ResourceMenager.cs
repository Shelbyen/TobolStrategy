using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    private static ResourceManager instance;
    private ResourceManager () {}

    public static ResourceManager GetInstance () {
        if (instance == null)
        {
            instance = new ResourceManager();
        }
        return instance;
    }

    private int Gold = 0; //Золото
    private int MaxHumans = 0; //Население
    private int HumansUsed; //Занятые мужики
    private int FaithPoints = 0; //Очки веры
    private int MaxFaith = 1000; //Максимум веры

    // for call method use: ResourceManager.GetInstance().Method()
    public int getCountGold () {
        return Gold;
    }
    public bool checkAndBuyGold (int cost) {
        if (Gold >= cost) {
            Gold -= cost;
            return true;
        }
        else {
            return false;
        }
    }
    public bool checkGold (int cost)
    {
        if (Gold >= cost) return true;
        else return false;
    }
    public void addGold (int count) {
        Gold += count;
    }
    public void setGold(int count)
    {
        Gold = count;
    }

    public void setMaxHumansCount(int count)
    {
        MaxHumans = count;
    }
    public int usedHumansCount()
    {
        return HumansUsed;
    }
    public int maxHumansCount()
    {
        return MaxHumans;
    }
    public void addMaxHumans(int count)
    {
        MaxHumans += count;
    }
    public void useHuman(int count)
    {
        HumansUsed += count;
    }

    public int getFaith()
    {
        return FaithPoints;
    }
    public bool checkFaith(int cost)
    {
        if (FaithPoints >= cost) return true;
        else return false;
    }
    public void addFaith(int count)
    {
        FaithPoints += count;
        FaithPoints = Mathf.Clamp(FaithPoints, 0, MaxFaith);
    }
}