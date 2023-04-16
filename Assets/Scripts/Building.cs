using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Building : MonoBehaviour
{
    public bool IsWrongPlace;
    public bool Placed;
    public bool Built;

    public float HP;
    public float BuildProgress;
    public int GoldCost;
    public int GoldMining;

    public GameObject Unit;
    public int UnitNumber;
    public GameObject Enter;

    private NavMeshObstacle Obstacle;
    private Builder BuilderScript;
    private int CollisionCount;
    private Material BaseMaterial;
    private Collider BuildingCollider;

    public void Awake()
    {
        Obstacle = GetComponent<NavMeshObstacle>();
        Obstacle.enabled = false;
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
        Obstacle.enabled = true;
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

        if (UnitNumber != 0) StartCoroutine(SpawnUnits());
    }

    public IEnumerator SpawnUnits()
    {
        int z = 0;
        while (z < UnitNumber)
        {
            z += 1;
            GameObject SpawnedUnit = Instantiate(Unit);
            SpawnedUnit.transform.position = Enter.transform.position;
            yield return new WaitForSeconds(1);
        }
    }
}