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
    public TMP_Text FunctionStats;
    public Image TikTimer;
    public TMP_Text UpgradeCost;
    public GameObject UnitCost;
    public TMP_Text UnitFree;

    public GameObject UnitControl;
    public Button Upgrade;
    public Button BuyUnit;
    public Button FireUnit;

    public GameObject UnitInfo;
    public TMP_Text UnitType;
    public TMP_Text Health;
    public TMP_Text Damage;
    public TMP_Text Speed;
    //Heal Units
    public GameObject HealInfo;
    public TMP_Text MaxHeal;
    public TMP_Text HealPerTik;
    //Gold Mining
    public GameObject GoldInfo;
    public TMP_Text GoldPerTik;

    public bool BlockRaycast;
    public GameObject SelectedObject;
    public LayerMask Mask;

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

            if (Building.Level < Building.UpgradeCost.Length && ((GameObject.FindWithTag("Remezov") != null && GameObject.FindWithTag("Remezov").GetComponent<Building>().Built) || Building.Level < 2) && ResourceManager.GetInstance().checkGold(Building.UpgradeCost[Building.Level]) && Building.Built) Upgrade.interactable = true;
            else Upgrade.interactable = false;

            ShowBuildingWorkers();
        }
    }

    private void ShowBuildingWorkers()
    {
        if (Building.GetComponent<SummonBuilding>()) UnitCheckSummon();
        else if (Building.GetComponent<Workplace>()) UnitCheckWorkplace();
        else if (Building.GetComponent<Church>()) UnitCheckChurch();
        else if (Building.GetComponent<House>()) UnitCheckHouse();
        else UnitDefault();
    }


    //Информация о юнитах в здании
    private void UnitCheckSummon()
    {
        SummonBuilding Summon = Building.GetComponent<SummonBuilding>();
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
    private void UnitCheckWorkplace()
    {
        Workplace Work = Building.GetComponent<Workplace>();
        FunctionStats.text = $"{"Рабочие: " + Work.WorkersCount + "/" + Work.MaxWorkers[Building.Level]}";
        if (Building.Built && Work.MaxWorkers[Building.Level] > Work.WorkersCount && ResourceManager.GetInstance().maxHumansCount() > ResourceManager.GetInstance().usedHumansCount())
        {
            BuyUnit.interactable = true;
        }
        else
        {
            BuyUnit.interactable = false;
        }

        if (Building.Built && Work.WorkersCount > 0)
        {
            FireUnit.interactable = true;
        }
        else
        {
            FireUnit.interactable = false;
        }
    }
    private void UnitCheckChurch()
    {
        Church Church = Building.GetComponent<Church>();
        FunctionStats.text = $"{"Молящиеся: " + Church.GetHumans()}";
    }
    private void UnitCheckHouse()
    {
        FunctionStats.text = $"{"Жители: " + SelectedObject.GetComponent<House>().ResidentsCount[Building.Level]}";
    }
    private void UnitDefault()
    {
        FunctionStats.text = $"{"Нет вакансий"}";
    }

    public void WindowAwake()
    {
        BuildingIcon.sprite = Building.BuildingTypeImage;
        BuildingType.text = SelectedObject.GetComponent<Selectable>().Name;
        DisableAllWindows();
        WindowUpdate();
        Window.SetActive(true);
    }

    private void WindowUpdate()
    {
        UpdateCostInfo();

        if (SelectedObject.GetComponent<Workplace>())
        {
            UpdateWorkplaceInfo();
            if (SelectedObject.GetComponent<HealBuilding>()) UpdateHealInfo();
            else if (SelectedObject.GetComponent<GoldMiningBuilding>()) UpdateMiningInfo();
        }
        else if (SelectedObject.GetComponent<SummonBuilding>()) UpdateSummonInfo();
    }

    private void UpdateSummonInfo()
    {
        SummonBuilding Summon = SelectedObject.GetComponent<SummonBuilding>();
        UnitCost.SetActive(true);
        UnitFree.gameObject.SetActive(false);
        UnitType.text = Summon.Unit.name;
        Health.text = $"{"Здоровье: " + Summon.UnitMaxHP[Building.Level]}";
        Speed.text = $"{"Скорость: " + Summon.UnitSpeed[Building.Level]}";
        Damage.text = $"{"Урон: " + Summon.UnitDamage[Building.Level]}";
        UnitCost.GetComponentInChildren<TMP_Text>().text = $"{Summon.UnitCost[Building.Level]}";
        UnitControl.SetActive(true);
        UnitInfo.SetActive(true);
    }

    private void UpdateWorkplaceInfo()
    {
        Workplace Work = SelectedObject.GetComponent<Workplace>();
        UnitCost.SetActive(false);
        UnitFree.gameObject.SetActive(true);
        UnitControl.SetActive(true);
    }

    private void UpdateHealInfo()
    {
        HealBuilding Heal = SelectedObject.GetComponent<HealBuilding>();
        MaxHeal.text = $"{"Макс: " + (Heal.MaxHeal[Building.Level] * 100) + "%"}";
        HealPerTik.text = $"{"Лечение: " + Heal.ReturnHealPower()}";
        HealInfo.SetActive(true);
    }

    private void UpdateMiningInfo()
    {
        GoldMiningBuilding Gold = SelectedObject.GetComponent<GoldMiningBuilding>();
        GoldPerTik.text = $"{"Доход: " + Gold.ReturnGoldMining()}";
        GoldInfo.SetActive(true);
    }

    private void UpdateCostInfo()
    {
        Level.text = $"{"Уровень: " + (Building.Level + 1)}";

        if (Building.Level < Building.UpgradeCost.Length)
        {
            UpgradeCost.text = $"{Building.UpgradeCost[Building.Level]}";
        }
        else
        {
            UpgradeCost.text = $"{"---"}";
            Upgrade.interactable = false;
        }
    }

    private void DisableAllWindows()
    {
        HealInfo.SetActive(false);
        GoldInfo.SetActive(false);
        UnitInfo.SetActive(false);
        //Контроль найма
        UnitControl.SetActive(false);
    }

    public void BuyNewUnit()
    {
        if (BuyUnit.interactable)
        {
            if (Building.GetComponent<SummonBuilding>()) Building.GetComponent<SummonBuilding>().BuyUnit();
            else Building.GetComponent<Workplace>().HireWorker();
            WindowUpdate();
        }
    }
    public void DeleteUnit()
    {
        if (FireUnit.interactable)
        {
            if (Building.GetComponent<SummonBuilding>()) Building.GetComponent<SummonBuilding>().DeleteUnit();
            else Building.GetComponent<Workplace>().FireWorker();
            WindowUpdate();
        }
    }
    public void UpgradeSelected()
    {
        if (Upgrade.interactable)
        {
            ResourceManager.GetInstance().checkAndBuyGold(Building.UpgradeCost[Building.Level]);
            Building.UpgradeThis();
            WindowUpdate();
        }
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
