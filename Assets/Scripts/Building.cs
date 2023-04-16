using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool IsWrongPlace;
    public bool Placed;
    public bool Built;

    public float HP;
    public float BuildProgress;
    public int GoldCost;

    public GameObject unit;

    private Builder BuilderScript;
    private int CollisionCount;
    private Material BaseMaterial;
    private Collider BuildingCollider;

    public void Awake()
    {
        BuilderScript = GameObject.Find("CameraObject").GetComponent<Builder>();
        BuildingCollider = GetComponent<Collider>();
        BaseMaterial = GetComponentsInChildren<Renderer>()[0].material;

        if (GoldCost <= ResourceManager.GetInstance().getCountGold() && CollisionCount <= 0) GoodPlace();
        else WrongPlace();

        if (!Placed) 
        {
            foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) 
            {
                renderer.material = BuilderScript.GoodMaterial;
            }
        }
    }

    public void FixedUpdate()
    {
        if (!Built)
        {
            if (Placed) BuildProgress += 10 * Time.fixedDeltaTime;
            if (BuildProgress >= 100) BuildThis();
        }
        if (!Placed)
        { 
            if (GoldCost <= ResourceManager.GetInstance().getCountGold() && CollisionCount <= 0) GoodPlace();
            else WrongPlace();
        }
    }

    public void OnTriggerEnter(Collider Collider)
    {
        if (!Placed)
        {
            CollisionCount += 1;
        }
    }
    public void OnTriggerExit(Collider Collider)
    {
        if (!Placed)
        {
            CollisionCount -= 1;
        }
    }

    public void WrongPlace()
    {
        Debug.Log("Good place or No money");
        IsWrongPlace = true;
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) 
        {
            renderer.material = BuilderScript.WrongMaterial;
        }
    }

    public void GoodPlace()
    {
        Debug.Log("All is good");
        IsWrongPlace = false;
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) 
        {
            renderer.material = BuilderScript.GoodMaterial;
        }
    }

    public void PlaceThis()
    {
        Placed = true;
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) 
        {
            renderer.material = BuilderScript.GoodMaterial;
        }
    }

    public void BuildThis()
    {
        Built = true;
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) 
        {
            renderer.material = BaseMaterial;
        }
    }
}
