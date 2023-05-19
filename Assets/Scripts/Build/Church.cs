using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Church : Building
{
    public int[] FaithPerHuman;

    public override void NewTik()
    {
        base.NewTik();
        AddFaith();
    }

    public void AddFaith()
    {
        ResourceManager.GetInstance().addFaith(GetHumans() * FaithPerHuman[Level]);
        Debug.Log(ResourceManager.GetInstance().getFaith());
    }

    public int GetHumans()
    {
        return ResourceManager.GetInstance().maxHumansCount() - ResourceManager.GetInstance().usedHumansCount();
    }
}
