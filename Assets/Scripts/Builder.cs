using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public GameManagerScript GameManager;

    public bool DestroyMode;
    public bool GridMode;
    public GameObject building;

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
        //re
        
    }
    public void StartBuilding()
    {
        GameManager.BlockRaycast = true;
        ActiveBuilding = Instantiate(building);
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
        ActiveBuilding.GetComponent<Building>().Placed = true;
        ActiveBuilding = null;
        Debug.Log("Building placed");
    }

    public void CancelBuilding()
    {
        Destroy(ActiveBuilding);
        Debug.Log("Cancel building");
    }
}
