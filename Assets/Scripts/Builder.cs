using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Builder : MonoBehaviour
{
    public GameManagerScript GameManager;
    public int Gold;
    public TMP_Text GoldCount;

    public bool DestroyMode;
    public bool GridMode;

    public Material WrongMaterial;
    public Material GoodMaterial;

    public GameObject ActiveBuilding;
    public RaycastHit BuilderHit;

    public GameObject Menu;
    //public Image GridButton;
    //public Image DestroyButton;
    public TMP_Text Info;

    public bool BuildMode;

    void Awake()
    {
        GameManager = GetComponent<GameManagerScript>();
        Menu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("f")) SiwtchbuildMode(!BuildMode);

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

        GoldCount.text = Gold.ToString();
    }

    public void HotKeys()
    {
        if (Input.GetKeyDown("delete") && ActiveBuilding == null) SwitchDestroyMode();
        if (Input.GetKeyDown("g")) SwitchGridMode();
    }

    public void SiwtchbuildMode(bool Status)
    {
        BuildMode = Status;
        Menu.SetActive(Status);
        GameManager.BlockRaycast = Status;
    }

    public void SwitchDestroyMode()
    {
        CancelBuilding();
        DestroyMode = !DestroyMode;
        Debug.Log("Destroy Switched");
        if (DestroyMode) Info.text = "Destroy mode";
        else Info.text = "";
    }

    public void SwitchGridMode()
    {
        GridMode = !GridMode;
        Debug.Log("Grid Switched");
    }

    public void MoveBuilding()
    {
        if (Physics.Raycast(GameManager.MainCamera.ScreenPointToRay(Input.mousePosition), out BuilderHit, 100f, 64))
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
        Info.text = "";
        CancelBuilding();
        ActiveBuilding = Instantiate(BuildingPrefab);

        Debug.Log("Building instantiated");
    }

    public void DestroyBuilding()
    {
        if (Physics.Raycast(GameManager.MainCamera.ScreenPointToRay(Input.mousePosition), out BuilderHit, 100f, 128))
        {
            if (!BuilderHit.transform.gameObject.GetComponent<Building>().Built) Gold += BuilderHit.transform.gameObject.GetComponent<Building>().GoldCost;
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
            Gold -= buildingComponent.GoldCost;
            ActiveBuilding = null;
            GameManager.BlockRaycast = false;

            Debug.Log("Building placed");
        }
    }

    public void CancelBuilding()
    {
        Destroy(ActiveBuilding);

        Debug.Log("Cancel building");
    }
}
