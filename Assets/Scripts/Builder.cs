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

    public bool BlockBuilder;
    private GameManagerScript GameManager;
    private Camera MainCamera;
    private RaycastHit BuilderHit;
    //Status vars
    private bool BuildMode;
    private bool DestroyMode;
    private bool GridMode;

    void Awake()
    {
        MainCamera = Camera.main;
        GameManager = GetComponent<GameManagerScript>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManagerScript>();
        UIManager.ChangeStatusBuildMenu(false);

        UIManager.ChangeStatusGoldCost(false);
        UIManager.ChangeTextGoldCost("");
    }

    void Update()
    {
        if (Input.GetKeyDown("f") && !BlockBuilder) SiwtchbuildMode(!BuildMode);

        if (BuildMode)
        {
            HotKeys();
            if (DestroyMode)
            {
                if (Input.GetMouseButtonDown(0)) DestroyBuilding();
            }
            if (ActiveBuilding != null)
            {
                MoveBuilding();
                if (Input.GetMouseButtonDown(1)) PlaceBuilding();
                if (Input.GetKeyDown("delete")) CancelBuilding();
            }
        }
    }

    public void Toggle()
    {
        SiwtchbuildMode(!BuildMode);
    }

    public void BlockBuild(bool Status)
    {
        CancelBuilding();
        BlockBuilder = Status;
        SiwtchbuildMode(!Status);
    }
    public void HotKeys()
    {
        if (Input.GetKeyDown("delete") && ActiveBuilding == null) SwitchDestroyMode();
        if (Input.GetKeyDown("g")) SwitchGridMode();
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

    public void SwitchDestroyMode()
    {
        CancelBuilding();
        DestroyMode = !DestroyMode;
        Debug.Log("Destroy Switched");
        if (DestroyMode) UIManager.ChangeTextStatusBar("Destroy mode");
        else UIManager.ChangeTextStatusBar("");
    }

    public void SwitchGridMode()
    {
        GridMode = !GridMode;
        Debug.Log("Grid Switched");
    }

    public void MoveBuilding()
    {
        if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out BuilderHit, 100f, 64))
        {
            if (GridMode) ActiveBuilding.transform.position = new Vector3 (Mathf.Round(BuilderHit.point.x), BuilderHit.point.y, Mathf.Round(BuilderHit.point.z));
            else ActiveBuilding.transform.position = BuilderHit.point;
        }

        if(Input.GetKeyDown(KeyCode.R)) {
            ActiveBuilding.transform.Rotate(Vector3.up * 45);
        }
    }

    public void StartBuilding(GameObject BuildingPrefab)
    {
        DestroyMode = false;
        CancelBuilding();
        ActiveBuilding = Instantiate(BuildingPrefab);

        UIManager.ChangeTextStatusBar(ActiveBuilding.GetComponent<Building>().Discription);
        UIManager.ChangeStatusGoldCost(true);
        UIManager.ChangeTextGoldCost(ActiveBuilding.GetComponent<Building>().GoldCost.ToString());

        Debug.Log("Building instantiated");
    }

    public void DestroyBuilding()
    {
        if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out BuilderHit, 100f, 128))
        {
            if (!BuilderHit.transform.gameObject.GetComponent<Building>().Built) {
                ResourceManager.GetInstance().addGold(BuilderHit.transform.gameObject.GetComponent<Building>().GoldCost);
            }
            BuilderHit.transform.gameObject.GetComponent<Building>().KillAll();
            Destroy(BuilderHit.transform.gameObject);

            Debug.Log("Building erased");
        }
    }

    public void PlaceBuilding()
    {
        Building buildingComponent = ActiveBuilding.GetComponent<Building>();
        
        if (!buildingComponent.IsWrongPlace)
        {
            buildingComponent.Placed = true;
            ResourceManager.GetInstance().checkAndBuyGold(buildingComponent.GoldCost);
            ActiveBuilding = null;
            GameManager.BlockRaycast = false;
            UIManager.ChangeTextStatusBar("");
            UIManager.ChangeStatusGoldCost(false);
            UIManager.ChangeTextGoldCost("");

            Debug.Log("Building placed");
        }
    }

    public void CancelBuilding()
    {
        Destroy(ActiveBuilding);
        UIManager.ChangeTextStatusBar("");
        UIManager.ChangeStatusGoldCost(true);
        UIManager.ChangeTextGoldCost("");

        Debug.Log("Cancel building");
    }
}
