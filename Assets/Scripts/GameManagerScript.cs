using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public bool BlockRaycast;
    public GameObject SelectedObject;
    public Image Timer;

    private Camera MainCamera;
    private float TimeLeft;
    private RaycastHit SelectingHit;
    private float TimeBeforeRaid;


    void Awake()
    {
        MainCamera = Camera.main;
    }

    void Update()
    {
        if (!BlockRaycast)
        {
            SelectObject();
        }
    }

    void FixedUpdate()
    {
        TimeLeft += Time.fixedDeltaTime / TimeBeforeRaid;
        Timer.fillAmount = TimeLeft;
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

            }
        }
        if (Input.GetKeyDown("escape") && SelectedObject != null)
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
        SelectedObject = null;
    }
}
