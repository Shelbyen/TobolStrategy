using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private RaycastHit Hit;
    public bool DestroyMode;

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
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit, 1000f, 128))
        {
            if (!Hit.transform.gameObject.GetComponent<Building>().Built)
            {
                ResourceManager.GetInstance().addGold(Hit.transform.gameObject.GetComponent<Building>().GoldCost);
            }
            Hit.transform.gameObject.GetComponent<Building>().KillAll();
            Destroy(Hit.transform.gameObject);
        }
    }
}
