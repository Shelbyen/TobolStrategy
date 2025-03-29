using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private bool BlockRaycast;
    [SerializeField] private GameObject SelectedObject;
    [SerializeField] private LayerMask Mask;

    private Camera MainCamera;
    private RaycastHit SelectingHit;
    private Building Building;

    private Ray Ray;

    void Awake()
    {
        LinkManager.Reload();
        MainCamera = Camera.main;
        LinkManager.GetUIManager().CloseStatWindows();
        LinkManager.GetUIManager().ChangeStatusBuildMenu(false);
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
            Building.ShowUpdatingStats();
            Building.ShowUnits();
        }
    }

    public void BlockRaycasting(bool status)
    {
        BlockRaycast = status;
    }

    public void BuyNewUnit()
    {
        Building.AddUnit();
        Building.ShowStats();
    }
    public void DeleteUnit()
    {
        Building.DeleteUnit();
        Building.ShowStats();
    }
    public void UpgradeSelected()
    {
        Building.UpgradeThis();
        Building.ShowStats();
    }
    public void DestroySelected()
    {
        Destroy(SelectedObject);
        LinkManager.GetUIManager().CloseStatWindows();
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
                Building = SelectedObject.GetComponent<Building>();
                Building.ShowStats();
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
            Debug.Log(Building.name + " is deselected");
        }
        LinkManager.GetUIManager().CloseStatWindows();
        SelectedObject = null;
    }
}
