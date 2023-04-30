using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private GameManagerScript GameManager;
    private UIManagerScript UIManager;
    private RaycastHit Hit;
    public bool DestroyMode;
    public LayerMask Layer;

    void Awake()
    {
        GameManager = GameObject.Find("Selector").GetComponent<GameManagerScript>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManagerScript>();
    }

    void Update()
    {
        if (InputManager.GetKeyDown("Select") && DestroyMode) DestroyBuilding();
    }

    public void SwitchDestroyMode()
    {
        DestroyMode = !DestroyMode;
        Debug.Log("Destroy Switched");
        if (DestroyMode) UIManager.ChangeDestroyImage(true);
        else UIManager.ChangeDestroyImage(false);
    }

    public void SetDestroyMode(bool Status)
    {
        DestroyMode = Status;
        Debug.Log("Destroy " + Status);
        UIManager.ChangeDestroyImage(Status);
    }

    public void DestroyBuilding()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit, 1000f, Layer))
        {
            Destroy(Hit.transform.gameObject);
        }
    }
}
