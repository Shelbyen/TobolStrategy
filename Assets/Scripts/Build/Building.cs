using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Building : MonoBehaviour
{
    public string Discription;
    public bool IsEnemy;
    public bool BuildOnStart;
    public int Level;

    [NonSerialized] public bool IsWrongPlace;
    [NonSerialized] public bool Placed;
    [NonSerialized] public bool Built;
    [NonSerialized] public float BuildProgress;

    public int GoldCost;
    public int[] UpgradeCost;
    public bool AnotherWorkMode;
    public float TikTimer;
    public float TimeLeft;

    private SummonBuilding Summon;
    private HealBuilding Heal;
    private MoneyBuilding Miner;

    private NavMeshObstacle Obstacle;
    private Builder BuilderScript;
    private int CollisionCount;
    private Material[] BaseMaterial;
    private Renderer[] Render;
    private Collider BuildingCollider;

    void Awake()
    {
        Summon = GetComponent<SummonBuilding>();
        Heal = GetComponent<HealBuilding>();
        Miner = GetComponent<MoneyBuilding>();

        Obstacle = GetComponent<NavMeshObstacle>();
        Obstacle.enabled = false;
        BuilderScript = GameObject.Find("Builder").GetComponent<Builder>();
        BuildingCollider = GetComponent<Collider>();
        Render = GetComponentsInChildren<Renderer>();
        BaseMaterial = new Material[Render.Length];
        for (int i = 0; i < Render.Length; i += 1)
        {
            BaseMaterial[i] = Render[i].material;
        }
        if (BuildOnStart) BuildThis();
        else CheckPlace();
    }

    void Update()
    {
        if (!Built)
        {
            if (Placed) BuildProgress += 10 * Time.deltaTime;
            if (BuildProgress >= 100) BuildThis();
        }
        else
        {
            if (!IsEnemy) Timer();
        }
        if (!Placed)
        {
            CheckPlace();
        }
    }

    void OnDestroy()
    {
        if (!Built && Placed) ResourceManager.GetInstance().addGold(GoldCost);
    }

    private void OnTriggerEnter(Collider Collider)
    {
        if (!Placed) CollisionCount += 1;
    }
    private void OnTriggerExit(Collider Collider)
    {
        if (!Placed) CollisionCount -= 1;
    }

    public void CheckPlace()
    {
        if (CollisionCount <= 0)
        {
            if (GoldCost <= ResourceManager.GetInstance().getCountGold())
            {
                GoodPlace();
            }
            else
            {
                Debug.Log("No money");
                WrongPlace();
            }
        }
        else
        {
            Debug.Log("Wrong place");
            WrongPlace();
        }
    }

    public void WrongPlace()
    {
        IsWrongPlace = true;
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) renderer.material = BuilderScript.WrongMaterial;
    }

    public void GoodPlace()
    {
        IsWrongPlace = false;
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) renderer.material = BuilderScript.GoodMaterial;
    }

    public void PlaceThis()
    {
        Placed = true;
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) renderer.material = BuilderScript.GoodMaterial;
        Obstacle.enabled = true;
    }

    public void BuildThis()
    {
        Obstacle.enabled = true;
        Placed = true;
        Built = true;
        for (int i = 0; i < Render.Length; i += 1) Render[i].material = BaseMaterial[i];
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    public void Timer()
    {
        TimeLeft += Time.deltaTime;
        if (TimeLeft >= TikTimer)
        {
            if (Heal != null) Heal.Heal(Level);
            if (Miner != null) Miner.GoldMine(Level);
            TimeLeft = 0;
        }
    }

    public void Upgrade()
    {
        if (Level < 2)
        {
            Level += 1;
            if (Summon != null) Summon.Upgrade();
        }
    }
}