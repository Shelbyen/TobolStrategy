using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public float HP;
    public Selectable SelectableScript;
    public RaycastHIt Hit;
    public Vactor3 Target;

    public void Awake() 
    {
        SelectableScript = GetComponent<Selectable>();
    }

    public void Update()
    {
        
    }
}
