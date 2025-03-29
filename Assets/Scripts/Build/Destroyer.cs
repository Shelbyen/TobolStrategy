using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private GameManagerScript GameManager;
    private RaycastHit Hit;
    public bool DestroyMode;
    [SerializeField] private LayerMask Layer;

    public ButtonSwitch DestroyButton;

    void Awake()
    {
        GameManager = GameObject.Find("Selector").GetComponent<GameManagerScript>();
    }

    void Update()
    {
        if (InputManager.GetKeyDown("Select") && DestroyMode) DestroyBuilding();
    }

    public void SwitchDestroyMode()
    {
        SetDestroyMode(!DestroyMode);
    }

    public void SetDestroyMode(bool Status)
    {
        DestroyMode = Status;
        Debug.Log("Destroy " + Status);
        DestroyButton.SetStatus(DestroyMode);
    }

    public void DestroyBuilding()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit, 1000f, Layer))
        {
            Destroy(Hit.transform.gameObject);
        }
    }
}
