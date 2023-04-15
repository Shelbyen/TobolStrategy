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

    public bool BuildMode;

    void Awake()
    {
        GameManager = GetComponent<GameManagerScript>();
        Menu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            if (BuildMode) SiwtchbuildMode(false);
            else SiwtchbuildMode(true);
        }

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

        GoldCount.text = $"{Gold}";
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
        DestroyMode = !DestroyMode;
        Debug.Log("Destroy Switched");
    }

    public void SwitchGridMode()
    {
        GridMode = !GridMode;
        Debug.Log("Grid Switched");
    }

    public void MoveBuilding()
    {
        if (Physics.Raycast(GameManager.MainCamera.ScreenPointToRay(Input.mousePosition), out BuilderHit, 100f, (1 << 6)))
        {
            if (GridMode) ActiveBuilding.transform.position = new Vector3 (Mathf.Round(BuilderHit.point.x), BuilderHit.point.y, Mathf.Round(BuilderHit.point.z));
            else ActiveBuilding.transform.position = BuilderHit.point;
        }

        if (Input.GetKeyDown("r")) Rotate45();
    }

    public void Rotate45()
    {
        ActiveBuilding.transform.Rotate(0, 45, 0);
    }

    public void StartBuilding(GameObject BuildingPrefab)
    {
        CancelBuilding();
        ActiveBuilding = Instantiate(BuildingPrefab);

        Debug.Log("Building instantiated");
    }

    public void DestroyBuilding()
    {
        if (Physics.Raycast(GameManager.MainCamera.ScreenPointToRay(Input.mousePosition), out BuilderHit, 100f, (1 << 7)))
        {
            Destroy(BuilderHit.transform.gameObject);

            Debug.Log("Building erased");
        }
    }

    public void PlaceBuilding()
    {
        if (!ActiveBuilding.GetComponent<Building>().IsWrongPlace)
        {
            ActiveBuilding.GetComponent<Building>().Placed = true;
            Gold -= ActiveBuilding.GetComponent<Building>().GoldCost;
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
