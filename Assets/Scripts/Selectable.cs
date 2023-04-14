using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public string Name;
    public bool Selected;

    public void SelectThis()
    {
        Selected = true;
    }

    public void DeselectThis()
    {
        Selected = false;
    }
}
