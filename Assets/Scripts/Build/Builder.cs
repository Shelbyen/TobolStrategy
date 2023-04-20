using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Builder : MonoBehaviour
{
    public Material WrongMaterial;
    public Material GoodMaterial;
    public GameObject ActiveBuilding;

    private UIManagerScript UIManager;
    private Destroyer Destroyer;

    public bool BlockBuilder;
    private GameManagerScript GameManager;
    private Camera MainCamera;
    private RaycastHit BuilderHit;
    //Status vars
    private bool BuildMode;
    private bool GridMode;

    void Awake()
    {
        MainCamera = Camera.main;
        Destroyer = GetComponent<Destroyer>();
        GameManager = GameObject.Find("Selector").GetComponent<GameManagerScript>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManagerScript>();
        UIManager.ChangeStatusBuildMenu(false);
        UIManager.ChangeStatusGoldCost(false);
        UIManager.ChangeTextGoldCost("");
    }

    void Update()
    {
        if (InputManager.GetKeyDown("BuildMode") && !BlockBuilder) SiwtchbuildMode(!BuildMode);

        if (BuildMode)
        {
            if (ActiveBuilding != null)
            {
                MoveBuilding();
                if (InputManager.GetKeyDown("Place") || InputManager.GetKeyDown("Accept")) PlaceBuilding();
                if (InputManager.GetKeyDown("Cancel")) CancelBuilding();
            }
            //Destroyer
            if (InputManager.GetKeyDown("DestroyMode") && ActiveBuilding == null)
            {
                CancelBuilding();
                Destroyer.SwitchDestroyMode();
                if (Destroyer.DestroyMode) UIManager.ChangeTextStatusBar("Destroy mode");
                else UIManager.ChangeTextStatusBar("");
            }
            //Grid
            if (InputManager.GetKeyDown("GridMode")) SwitchGridMode();
        }
    }

    public void Toggle()
    {
        SiwtchbuildMode(!BuildMode);
    }

    public void SiwtchbuildMode(bool Status)
    {
        CancelBuilding();
        BuildMode = Status;
        UIManager.ChangeStatusBuildMenu(Status);
        GameManager.BlockRaycast = Status;
        if (BuildMode) UIManager.ChangeTextToggleText("View");
        else UIManager.ChangeTextToggleText("Build");
    }

    public void SwitchGridMode()
    {
        GridMode = !GridMode;
        Debug.Log("Grid Switched");
    }

    public void MoveBuilding()
    {
        if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out BuilderHit, 1000f, 64))
        {
            if (GridMode) ActiveBuilding.transform.position = new Vector3 (Mathf.Round(BuilderHit.point.x), BuilderHit.point.y, Mathf.Round(BuilderHit.point.z));
            else ActiveBuilding.transform.position = BuilderHit.point;
        }
        else if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out BuilderHit, 1000f, 8))
        {
            if (GridMode) ActiveBuilding.transform.position = new Vector3(Mathf.Round(BuilderHit.point.x), BuilderHit.point.y, Mathf.Round(BuilderHit.point.z));
            else ActiveBuilding.transform.position = BuilderHit.point;
            ActiveBuilding.GetComponent<Building>().WrongPlace();
        }

        if (InputManager.GetKeyDown("RotateBuilding")) {
            ActiveBuilding.transform.Rotate(Vector3.up * 45);
        }
    }

    public void StartBuilding(GameObject BuildingPrefab)
    {
        Destroyer.DestroyMode = false;
        CancelBuilding();
        ActiveBuilding = Instantiate(BuildingPrefab);

        UIManager.ChangeTextStatusBar(ActiveBuilding.GetComponent<Building>().Discription);
        UIManager.ChangeStatusGoldCost(true);
        UIManager.ChangeTextGoldCost(ActiveBuilding.GetComponent<Building>().GoldCost.ToString());
    }

    public void PlaceBuilding()
    {
        Building buildingComponent = ActiveBuilding.GetComponent<Building>();
        
        if (!buildingComponent.IsWrongPlace)
        {
            buildingComponent.PlaceThis();
            ResourceManager.GetInstance().checkAndBuyGold(buildingComponent.GoldCost);
            ActiveBuilding = null;
            GameManager.BlockRaycast = false;
            UIManager.ChangeTextStatusBar("");
            UIManager.ChangeStatusGoldCost(false);
            UIManager.ChangeTextGoldCost("");
        }
    }

    public void CancelBuilding()
    {
        Destroy(ActiveBuilding);
        UIManager.ChangeTextStatusBar("");
        UIManager.ChangeStatusGoldCost(false);
        UIManager.ChangeTextGoldCost("");
    }
}