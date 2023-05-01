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

    public Button Upgrade;
    public Button BuyUnit;

    public GameObject UnitInfo;
    public TMP_Text UnitType;
    public TMP_Text Health;
    public TMP_Text Damage;
    public TMP_Text Speed;
    public TMP_Text Bullet;

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
                if (ResourceManager.GetInstance().getCountGold() >= Summon.UnitCost[Building.Level] && Building.Built && Summon.MaxUnitNumber[Building.Level] > Summon.BuildingsUnits.Count)
                {
                    BuyUnit.interactable = true;
                }
                else
                {
                    BuyUnit.interactable = false;
                }
            }
        }
    }

    public void WindowAwake()
    {
        BuildingType.text = SelectedObject.GetComponent<Selectable>().Name;
        Level.text = $"{"Level " + (Building.Level + 1)}";
        UnitInfo.SetActive(false);

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
            FunctionStats.text = $"{"Healing: " + SelectedObject.GetComponent<HealBuilding>().HealthPerHeal[Building.Level]}";
        else if (SelectedObject.GetComponent<MoneyBuilding>())
            FunctionStats.text = $"{"Mining: " + SelectedObject.GetComponent<MoneyBuilding>().GoldMining[Building.Level]}";
        else if (SelectedObject.GetComponent<SummonBuilding>())
        {
            UnitInfo.SetActive(true);
            SummonBuilding Summon = SelectedObject.GetComponent<SummonBuilding>();
            FunctionStats.text = $"{"Units: " + Summon.MaxUnitNumber[Building.Level]}";
            UnitType.text = Summon.Unit.name;
            Health.text = $"{"Health: " + Summon.UnitMaxHP[Building.Level]}";
            Speed.text = $"{"Speed: " + Summon.UnitSpeed[Building.Level]}";
            Damage.text = $"{"Damage: " + Summon.UnitDamage[Building.Level]}";
            Bullet.text = $"{"Bullet: " + Summon.BulletDamage[Building.Level]}";
            UnitCost.text = $"{Summon.UnitCost[Building.Level]}";
        }
        Window.SetActive(true);
    }

    public void BuyNewUnit()
    {
        Building.GetComponent<SummonBuilding>().BuyUnit();
    }

    public void UpgradeSelected()
    {
        ResourceManager.GetInstance().checkAndBuyGold(Building.UpgradeCost[Building.Level]);
        Building.Upgrade();
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
