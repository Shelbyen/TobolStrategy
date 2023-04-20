using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public bool BlockRaycast;
    public GameObject SelectedObject;
    private Camera MainCamera;
    private RaycastHit SelectingHit;
    private UIManagerScript UIManager;

    void Awake()
    {
        MainCamera = Camera.main;
        UIManager = GameObject.Find("UIManager").GetComponent<UIManagerScript>();
        UIManager.ChangeStatusBuildMenu(false);
    }

    void Update()
    {
        if (!BlockRaycast)
        {
            SelectObject();
        }
    }

    public void SelectObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DeselectObject();
            if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out SelectingHit) && SelectingHit.transform.gameObject.GetComponent<Selectable>())
            {   
                SelectedObject = SelectingHit.transform.gameObject;
                SelectedObject.GetComponent<Selectable>().SelectThis();
                Debug.Log(SelectedObject.GetComponent<Selectable>().Name + " is selected");

                UIManager.ChangeTextStatusBar(SelectedObject.GetComponent<Selectable>().Name + " selected");

            }
        }
        if (InputManager.GetKeyDown("Cancel") && SelectedObject != null)
        {
            DeselectObject();
        }
    }
    
    public void HotKeys()
    {

    }

    public void DeselectObject()
    {
        if (SelectedObject != null)
        {
            Debug.Log(SelectedObject.GetComponent<Selectable>().Name + " is deselected");
            SelectedObject.GetComponent<Selectable>().DeselectThis();
        }
        UIManager.ChangeTextStatusBar("");
        SelectedObject = null;
    }
}
