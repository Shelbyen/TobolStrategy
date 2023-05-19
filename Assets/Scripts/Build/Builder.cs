using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Builder : MonoBehaviour
{
    public int GridStep = 1;
    public LayerMask Ground;
    public Material WrongMaterial;
    public Material GoodMaterial;
    public GameObject ActiveBuilding;
    private BuildingButton BuildingButton;

    private UIManagerScript UIManager;
    private Destroyer Destroyer;
    private Ray Ray;

    private GameManagerScript GameManager;
    private Camera MainCamera;
    private RaycastHit Hit;

    private bool BuildMode;
    private bool GridMode;
    private bool GoodPlace;

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
        Ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        if (InputManager.GetKeyDown("BuildMode")) SiwtchbuildMode(!BuildMode);

        if (BuildMode)
        {
            if (ActiveBuilding != null)
            {
                MoveBuilding();
                CheckBuildingFactors();
                if (InputManager.GetKeyDown("Place") || InputManager.GetKeyDown("Accept")) PlaceBuilding();
                if (InputManager.GetKeyDown("Cancel")) CancelBuilding();
            }
            //Destroyer
            if (InputManager.GetKeyDown("DestroyMode") && ActiveBuilding == null)
            {
                CancelBuilding();
                Destroyer.SwitchDestroyMode();
                if (Destroyer.DestroyMode) UIManager.ChangeDestroyImage(true);
                else UIManager.ChangeDestroyImage(false);
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
        if (BuildMode) UIManager.ChangeBuildToggleImage(false);
        else UIManager.ChangeBuildToggleImage(true);

        GameManager.BlockRaycast = Status;
        Destroyer.SetDestroyMode(false);
    }

    public void SwitchGridMode()
    {
        GridMode = !GridMode;
        Debug.Log("Grid Switched");
        if (GridMode) UIManager.ChangeGridImage(true);
        else UIManager.ChangeGridImage(false);
    }

    public void MoveBuilding()
    {
        Physics.Raycast(Ray, out Hit, 1000f, Ground);
        if (GridMode) ActiveBuilding.transform.position = new Vector3(
            Mathf.Round(Hit.point.x / GridStep) * GridStep,
            Hit.point.y, 
            Mathf.Round(Hit.point.z / GridStep) * GridStep);
        else ActiveBuilding.transform.position = Hit.point;

        if (InputManager.GetKeyDown("RotateBuilding"))
        {
            ActiveBuilding.transform.Rotate(Vector3.up * 45);
        }
    }

    public void CheckBuildingFactors()
    {
        Building buildingComponent = ActiveBuilding.GetComponent<Building>();

        GoodPlace = buildingComponent.CheckPlace();
        if (Physics.Raycast(Ray, 1000f, Ground))
        {
            if (    !(Hit.transform.gameObject.layer == LayerMask.NameToLayer("FortressGround") || ActiveBuilding.GetComponent<Flagstaff>())  )
            {
                Debug.Log("Wrong place");
                GoodPlace = false;
            }
            else if (!ResourceManager.GetInstance().checkGold(BuildingButton.Cost))
            {
                Debug.Log("No Money");
                Debug.Log(BuildingButton.Cost);
                GoodPlace = false;
            }
        }
        else GoodPlace = false;

        if (GoodPlace) buildingComponent.SetMaterial(GoodMaterial);
        else buildingComponent.SetMaterial(WrongMaterial);
    }

    public void StartBuilding(GameObject BuildingPrefab, int Cost, BuildingButton Spawner)
    {
        Destroyer.DestroyMode = false;
        CancelBuilding();
        BuildingButton = Spawner;
        ActiveBuilding = Instantiate(BuildingPrefab);
        ActiveBuilding.GetComponent<Building>().SetCost(Cost);

        UIManager.ChangeStatusGoldCost(true);
        UIManager.ChangeTextGoldCost(Cost.ToString());
    }

    public void PlaceBuilding()
    {
        if (GoodPlace)
        {
            BuildingButton.StructureBuilt(ActiveBuilding);
            ActiveBuilding.GetComponent<Building>().PlaceThis();
            ActiveBuilding = null;

            UIManager.ChangeStatusGoldCost(false);
            UIManager.ChangeTextGoldCost("");
            if (Input.GetKey(KeyCode.LeftShift))
            {
                BuildingButton.SpawnBuilding();
            }
        }
    }

    public void CancelBuilding()
    {
        Destroy(ActiveBuilding);
        UIManager.ChangeStatusGoldCost(false);
        UIManager.ChangeTextGoldCost("");
    }
}