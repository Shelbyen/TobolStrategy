using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private RaycastHit Hit;
    public bool DestroyMode;
    public LayerMask Layer;

    void Update()
    {
        if (InputManager.GetKeyDown("Select") && DestroyMode) DestroyBuilding();
    }

    public void SwitchDestroyMode()
    {
        DestroyMode = !DestroyMode;
        Debug.Log("Destroy Switched");
    }

    public void SetDestroyMode(bool Status)
    {
        DestroyMode = Status;
        Debug.Log("Destroy " + Status);
    }

    public void DestroyBuilding()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit, 1000f, Layer))
        {
            Destroy(Hit.transform.gameObject);
        }
    }
}
