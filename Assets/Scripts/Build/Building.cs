using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Building : MonoBehaviour
{
    public Sprite BuildingTypeImage;
    public string Discription;
    public int Level;

    //[NonSerialized] public bool IsWrongPlace;
    public bool FreeBuild;
    [NonSerialized] public bool Placed;
    [NonSerialized] public bool Built;
    [NonSerialized] public float BuildProgress;

    public int[] UpgradeCost;
    public float TikTimer;
    public float TimeLeft;

    private SummonBuilding Summon;
    private HealBuilding Heal;
    private MoneyBuilding Miner;
    private House House;

    private int GoldCost;
    private NavMeshObstacle Obstacle;
    private Builder BuilderScript;
    private int CollisionCount;
    private Material[] BaseMaterial;
    private Renderer[] Render;
    private Collider BuildingCollider;
    //private BuildingButton BuildingButton;

    void Awake()
    {
        Summon = GetComponent<SummonBuilding>();
        Heal = GetComponent<HealBuilding>();
        Miner = GetComponent<MoneyBuilding>();
        House = GetComponent<House>();

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
        CheckPlace();
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
            if (TikTimer != 0) Timer();
        }
    }

    void OnDestroy()
    {
        if (!Built && Placed) ResourceManager.GetInstance().addGold(GoldCost);
        if (Built && House != null) House.DeleteResidents(Level);
    }

    private void OnTriggerEnter()
    {
        if (!Placed) CollisionCount += 1;
    }
    private void OnTriggerExit()
    {
        if (!Placed) CollisionCount -= 1;
    }

    public bool CheckPlace()
    {
        if (CollisionCount == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetMaterial(Material material)
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) renderer.material = material;
    }

    public void PlaceThis()
    {
        Placed = true;
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) renderer.material = BuilderScript.GoodMaterial;
        ResourceManager.GetInstance().checkAndBuyGold(GoldCost);
        Obstacle.enabled = true;
    }

    public void BuildThis()
    {
        Obstacle.enabled = true;
        Placed = true;
        Built = true;
        for (int i = 0; i < Render.Length; i += 1) Render[i].material = BaseMaterial[i];

        if (House != null) House.AddResidents(Level);
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

    public void UpgradeThis()
    {

        if (Level < 2)
        {
            Level += 1;
            if (Summon != null)
            {
                Summon.UpgradeUnits();
            }
            if (House != null)
            {
                House.DeleteResidents(Level - 1);
                House.AddResidents(Level);
            }
        }
    }

    public void SetCost(int Cost)
    {
        GoldCost = Cost;
    }
}