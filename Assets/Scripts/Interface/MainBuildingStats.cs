using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainBuildingStats : WindowStats
{
    [SerializeField] private Image BuildingIcon;
    [SerializeField] private Image Healthbar;
    [SerializeField] private Image TikTimer;
    [SerializeField] private Image UnitPrice;

    [SerializeField] private TMP_Text BuildingType;
    [SerializeField] private TMP_Text Level;
    [SerializeField] private TMP_Text UnitStats;
    [SerializeField] private TMP_Text UpgradeCost;
    [SerializeField] private TMP_Text UnitCost;
    [SerializeField] private TMP_Text FreeUnit;

    [SerializeField] private Button Upgrade;
    [SerializeField] private Button HireUnit;
    [SerializeField] private Button FireUnit;


    public void SetSprite(Sprite icon)
    {
        BuildingIcon.sprite = icon;
    }
    public void SetType(string type)
    {
        BuildingType.text = Localization.ReturnTranslation(type);
    }
    public void SetLevel(int lv)
    {
        Level.text = Localization.ReturnTranslation("level") + " " + lv.ToString();
    }
    public void SetUnitsInfo(string info, string count)
    {
        UnitStats.text = Localization.ReturnTranslation(info) + " " + count;
    }
    public void SetTimer(float percent)
    {
        TikTimer.fillAmount = percent;
    }
    public void SetHealth(float hp)
    {
        Healthbar.fillAmount = hp / 100;
    }
    public void SetUpgradeCost(string cost)
    {
        UpgradeCost.text = cost;
    }
    public void SetUpgrade(bool status)
    {
        Upgrade.interactable = status;
    }

    public void HireUnits(bool status)
    {
        HireUnit.interactable = status;
    }
    public bool HireStatus()
    {
        return HireUnit.interactable;
    }
    public void FireUnits(bool status)
    {
        FireUnit.interactable = status;
    }
    public bool FireStatus()
    {
        return FireUnit.interactable;
    }
    public void SetUnitCost(int cost)
    {
        if (cost != 0)
        {
            UnitPrice.gameObject.SetActive(true);
            FreeUnit.gameObject.SetActive(false);
        }
        else
        {
            UnitPrice.gameObject.SetActive(false);
            FreeUnit.gameObject.SetActive(true);
        }
        UnitCost.text = cost.ToString();
    }
}

