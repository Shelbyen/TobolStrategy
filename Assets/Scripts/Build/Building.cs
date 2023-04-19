using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Building : MonoBehaviour
{
    public string Discription;

    public bool IsEnemy;
    public bool BuildOnStart;

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
    private Material[] BaseMaterial;
    private Renderer[] Render;
    private Collider BuildingCollider;
    private GameObject[] BuildingsUnits;

    public void Awake()
    {
        Obstacle = GetComponent<NavMeshObstacle>();
        Obstacle.enabled = false;
        BuilderScript = Camera.main.transform.parent.gameObject.GetComponent<Builder>();
        BuildingCollider = GetComponent<Collider>();
        Render = GetComponentsInChildren<Renderer>();
        BaseMaterial = new Material[Render.Length];
        BuildingsUnits = new GameObject[UnitNumber];
        for (int i = 0; i < Render.Length; i += 1)
        {
            BaseMaterial[i] = Render[i].material;
        }

        if (BuildOnStart)
        {
            Obstacle.enabled = true;
            Placed = true;
            BuildThis();
        }
        else
        {
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
        Obstacle.enabled = true;
    }

    public void BuildThis()
    {
        Built = true;
        for (int i = 0; i < Render.Length; i += 1)
        {
            Render[i].material = BaseMaterial[i];
        }
        if (GoldMining != 0) StartCoroutine(GoldMine());
        if (UnitNumber != 0 && !IsEnemy) StartCoroutine(SpawnUnits());
        if (GetComponent<HealBuilding>()) GetComponent<HealBuilding>().StartHeal();
    }

    public IEnumerator SpawnUnits()
    {
        KillAll();
        int z = 0;
        while (z < UnitNumber)
        {
            BuildingsUnits[z] = Instantiate(Unit);
            BuildingsUnits[z].transform.position = Enter.transform.position;
            BuildingsUnits[z].GetComponent<Human>().Target = Enter.transform.position;
            z += 1;
            yield return new WaitForSeconds(1);
        }
    }

    public void KillAll()
    {
        foreach(GameObject UnitForKill in BuildingsUnits)
        {
            Destroy(UnitForKill);
        }
    }

    public IEnumerator GoldMine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            if (Camera.main.gameObject.GetComponentInParent<StateManager>().getState()) ResourceManager.GetInstance().addGold(GoldMining);
        }
    }
}