using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public TMP_Text TikTimerInfo;

    public Button Upgrade;
    public Button Cancel;
    public Button Destroy;

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
            TikTimerInfo.text = $"{"Next effect: " + Mathf.Round(Building.TikTimer - Building.TimeLeft) + "s"}";
        }
    }

    public void WindowAwake()
    {
        BuildingType.text = SelectedObject.GetComponent<Selectable>().Name;
        Level.text = $"{"Level " + (Building.Level + 1)}";

        if (SelectedObject.GetComponent<HealBuilding>()) 
            FunctionStats.text = $"{"Healing: " + SelectedObject.GetComponent<HealBuilding>().HealthPerHeal[Building.Level]}";
        else if (SelectedObject.GetComponent<MoneyBuilding>())
            FunctionStats.text = $"{"Mining: " + SelectedObject.GetComponent<MoneyBuilding>().GoldMining[Building.Level]}";
        else if (SelectedObject.GetComponent<SummonBuilding>())
            FunctionStats.text = $"{"Units: " + SelectedObject.GetComponent<SummonBuilding>().UnitNumber}";
    }

    public void UpgradeSelected()
    {
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
                Window.SetActive(true);
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
