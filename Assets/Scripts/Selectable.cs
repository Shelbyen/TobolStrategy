using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public string Name;
    public bool Selected;
    private Building Building;

    public void Awake()
    {
        Building = GetComponent<Building>();
    }

    public void SelectThis()
    {
        Selected = true;
        //Building.SetWindowStatus(true);
    }

    public void DeselectThis()
    {
        Selected = false;
        //Building.SetWindowStatus(false);
    }
}
