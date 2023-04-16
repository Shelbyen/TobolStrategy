using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour
{
    public float HP;
    public Vector3 Target;

    private Selectable SelectableScript;
    private RaycastHit Hit;
    private Camera MainCamera;
    private NavMeshAgent Agent;



    public void Awake() 
    {
        Agent = GetComponent<NavMeshAgent>();
        MainCamera = Camera.main;
        SelectableScript = GetComponent<Selectable>();
    }

    public void Update()
    {
        if (SelectableScript.Selected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out Hit, 100f, 64))
                {
                    Target = Hit.point;
                }
            }
        }


    }
    public void FixedUpdate()
    { 
        Agent.SetDestination(Target);
    }
}
