using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Selectable : MonoBehaviour
{
    public string Name;
    public bool Selected;

    private Building Building;

    public GameObject StatusWindow;
    public Vector3 StatusWindowPosition;

    public void Awake()
    {
        Building = GetComponent<Building>();
        if (StatusWindow != null)
        {
            StatusWindow.transform.SetParent(GameObject.Find("Canvas").transform);
            StatusWindow.SetActive(false);
        }
    }

    public void Update()
    {
        if (StatusWindow != null) StatusWindow.transform.position = transform.position + StatusWindowPosition;
    }

    public void SelectThis()
    {
        Selected = true;
        if (StatusWindow != null) StatusWindow.SetActive(true);
    }

    public void DeselectThis()
    {
        Selected = false;
        if (StatusWindow != null) StatusWindow.SetActive(false);
    }
}
