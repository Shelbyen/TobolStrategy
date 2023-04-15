using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool IsWrongPlace;
    public bool Placed;
    public bool Built;

    public Material BaseMaterial;
    public Collider BuildingCollider;
    public Builder BuilderScript;

    public float HP;
    public float BuildProgress;

    public int GoldCost;

    private int CollisionCount;

    public void Awake()
    {
        BuilderScript = GameObject.Find("CameraObject").GetComponent<Builder>();
        BuildingCollider = GetComponent<Collider>();
        BaseMaterial = GetComponentsInChildren<Renderer>()[0].material;

        if (GoldCost <= BuilderScript.Gold) WrongPlace();
        else GoodPlace();

        if (!Placed) for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; i += 1)
        {
            GetComponentsInChildren<Renderer>()[i].material = BuilderScript.GoodMaterial;
        }
    }

    public void FixedUpdate()
    {
        if (!Built)
        {
            if (Placed) BuildProgress += 1;
            if (BuildProgress >= 100) BuildThis();

            if (GoldCost <= BuilderScript.Gold) WrongPlace();
            else GoodPlace();
        }
    }

    public void OnTriggerEnter(Collider Collider)
    {
        if (!Placed)
        {
            CollisionCount += 1;
            WrongPlace();
        }
    }
    public void OnTriggerExit(Collider Collider)
    {
        if (!Placed)
        {
            CollisionCount -= 1;
            if (CollisionCount == 0) GoodPlace();
        }
    }

    public void WrongPlace()
    {
        Debug.Log("Wrong place");
        IsWrongPlace = true;
        for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; i += 1)
        {
            GetComponentsInChildren<Renderer>()[i].material = BuilderScript.WrongMaterial;
        }
    }

    public void GoodPlace()
    {
        Debug.Log("Good place");
        IsWrongPlace = false;
        for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; i += 1)
        {
            GetComponentsInChildren<Renderer>()[i].material = BuilderScript.GoodMaterial;
        }
    }

    public void PlaceThis()
    {
        Placed = true;
        for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; i += 1)
        {
            GetComponentsInChildren<Renderer>()[i].material = BaseMaterial;
        }
    }

    public void BuildThis()
    {
        Built = true;
    }
}
