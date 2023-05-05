using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    //UI
    public GameObject Window;

    public Image BuildingIcon;
    public TMP_Text BuildingType;
    public TMP_Text Level;
    public Image Healthbar;
    public Image FunctionIcon;
    public TMP_Text FunctionStats;
    public Image TikTimer;
    public TMP_Text UpgradeCost;
    public TMP_Text UnitCost;

    public GameObject UnitControl;
    public Button Upgrade;
    public Button BuyUnit;
    public Button FireUnit;

    public GameObject UnitInfo;
    public TMP_Text UnitType;
    public TMP_Text Health;
    public TMP_Text Damage;
    public TMP_Text Speed;
    public TMP_Text Bullet;

    public Sprite Units_Icon;
    public Sprite Heal_Icon;
    public Sprite Money_Icon;

    public bool BlockRaycast;
    public GameObject SelectedObject;
    public LayerMask Mask;
    public LayerMask UIMask;

    private Camera MainCamera;
    private RaycastHit SelectingHit;
    private UIManagerScript UIManager;
    private Building Building;

    private Ray Ray;

    void Awake()
    {
        Window.SetActive(false);
        MainCamera = Camera.main;
        UIManager = GameObject.Find("UIManager").GetComponent<UIManagerScript>();
        UIManager.ChangeStatusBuildMenu(false);
    }

    void Update()
    {
        if (!BlockRaycast)
        {
            Ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            SelectObject();
        }

        if (SelectedObject != null)
        {
            Healthbar.fillAmount = Building.BuildProgress / 100;
            TikTimer.fillAmount = Building.TimeLeft / Building.TikTimer;

            if (Building.Level < 2)
            {
                if (ResourceManager.GetInstance().getCountGold() >= Building.UpgradeCost[Building.Level] && Building.Built)
                {
                    Upgrade.interactable = true;
                }
                else
                {
                    Upgrade.interactable = false;
                }
            }


            if (Building.GetComponent<SummonBuilding>())
            {
                SummonBuilding Summon = Building.GetComponent<SummonBuilding>();
                FunctionIcon.sprite = Units_Icon;
                FunctionStats.text = $"{"Воины: " + Summon.BuildingsUnits.Count + "/" + Summon.MaxUnitNumber[Building.Level]}";
                if (ResourceManager.GetInstance().getCountGold() >= Summon.UnitCost[Building.Level] && Building.Built && Summon.MaxUnitNumber[Building.Level] > Summon.BuildingsUnits.Count && ResourceManager.GetInstance().maxHumansCount() > ResourceManager.GetInstance().usedHumansCount())
                {
                    BuyUnit.interactable = true;
                }
                else
                {
                    BuyUnit.interactable = false;
                }

                if (Building.Built && Summon.BuildingsUnits.Count > 0)
                {
                    FireUnit.interactable = true;
                }
                else
                {
                    FireUnit.interactable = false;
                }
            }
        }
    }

    public void WindowAwake()
    {
        BuildingIcon.sprite = Building.BuildingTypeImage;
        BuildingType.text = SelectedObject.GetComponent<Selectable>().Name;
        Level.text = $"{"Уровень: " + (Building.Level + 1)}";
        UnitInfo.SetActive(false);
        UnitControl.SetActive(false);

        if (Building.Level < 2)
        {
            UpgradeCost.text = $"{Building.UpgradeCost[Building.Level]}";
        }
        else
        {
            UpgradeCost.text = $"{"---"}";
            Upgrade.interactable = false;
        }


        if (SelectedObject.GetComponent<HealBuilding>())
        {
            FunctionStats.text = $"{"Лечение: " + SelectedObject.GetComponent<HealBuilding>().HealthPerHeal[Building.Level]}";
            FunctionIcon.sprite = Heal_Icon;
        }
        else if (SelectedObject.GetComponent<MoneyBuilding>())
        {
            FunctionStats.text = $"{"Доход: " + SelectedObject.GetComponent<MoneyBuilding>().GoldMining[Building.Level]}";
            FunctionIcon.sprite = Money_Icon;
        }
        else if (SelectedObject.GetComponent<House>())
        {
            FunctionStats.text = $"{"Жители: " + SelectedObject.GetComponent<House>().ResidentsCount[Building.Level]}";
            FunctionIcon.sprite = Units_Icon;
        }
        else if (SelectedObject.GetComponent<SummonBuilding>())
        {
            UnitControl.SetActive(true);
            UnitInfo.SetActive(true);
            SummonBuilding Summon = SelectedObject.GetComponent<SummonBuilding>();
            UnitType.text = Summon.Unit.name;
            Health.text = $"{"Здоровье: " + Summon.UnitMaxHP[Building.Level]}";
            Speed.text = $"{"Скорость: " + Summon.UnitSpeed[Building.Level]}";
            Damage.text = $"{"Урон: " + Summon.UnitDamage[Building.Level]}";
            UnitCost.text = $"{Summon.UnitCost[Building.Level]}";
        }
        Window.SetActive(true);
    }

    public void BuyNewUnit()
    {
        Building.GetComponent<SummonBuilding>().BuyUnit();
    }
    public void DeleteUnit()
    {
        Building.GetComponent<SummonBuilding>().DeleteUnit();
    }

    public void UpgradeSelected()
    {
        ResourceManager.GetInstance().checkAndBuyGold(Building.UpgradeCost[Building.Level]);
        Building.UpgradeThis();
        WindowAwake();
    }

    public void DestroySelected()
    {
        Destroy(SelectedObject);
        Window.SetActive(false);
        SelectedObject = null;
    }

    public void SelectObject()
    {
        if (InputManager.GetKeyDown("Select"))
        {
            if (Physics.Raycast(Ray, out SelectingHit, 1000f, Mask))
            {
                DeselectObject();
                SelectedObject = SelectingHit.transform.gameObject;
                SelectedObject.GetComponent<Selectable>().SelectThis();
                Building = SelectedObject.GetComponent<Building>();
                WindowAwake();
            }
        }
        if (InputManager.GetKeyDown("Cancel") && SelectedObject != null)
        {
            DeselectObject();
        }
    }

    public void DeselectObject()
    {
        if (SelectedObject != null)
        {
            Debug.Log(SelectedObject.GetComponent<Selectable>().Name + " is deselected");
            SelectedObject.GetComponent<Selectable>().DeselectThis();
        }
        Window.SetActive(false);
        SelectedObject = null;
    }
}
