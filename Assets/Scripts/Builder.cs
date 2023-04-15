using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void Awake()
    {
        GameManager = GetComponent<GameManagerScript>();
    }

    void Update()
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
            if (Input.GetMouseButtonDown(0)) CancelBuilding();
        }

        GoldCount.text = $"{"Gold: " + Gold}";
    }

    public void HotKeys()
    {
        if (Input.GetKeyDown("delete")) DestroyMode = !DestroyMode;
        if (Input.GetKeyDown("g")) GridMode = !GridMode;
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
        GameManager.BlockRaycast = true;
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
            ActiveBuilding = null;

            Debug.Log("Building placed");
        }
    }

    public void CancelBuilding()
    {
        Destroy(ActiveBuilding);

        Debug.Log("Cancel building");
    }
}
