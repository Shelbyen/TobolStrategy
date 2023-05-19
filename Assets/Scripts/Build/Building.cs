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

    [NonSerialized] public bool Placed;
    [NonSerialized] public bool Built;
    [NonSerialized] public float BuildProgress;

    public int[] UpgradeCost;
    public float TikTimer;
    public float TimeLeft;

    protected int GoldCost;
    protected NavMeshObstacle Obstacle;
    protected Builder BuilderScript;
    protected int CollisionCount;
    protected Material[] BaseMaterial;
    protected Renderer[] Render;
    protected Collider BuildingCollider;

    public virtual void Awake()
    {
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

    public void Update()
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

    public virtual void OnDestroy()
    {
        if (Placed && !Built) ResourceManager.GetInstance().addGold(GoldCost);
    }

    public void OnTriggerEnter()
    {
        if (!Placed) CollisionCount += 1;
    } //Collision+
    public void OnTriggerExit()
    {
        if (!Placed) CollisionCount -= 1;
    } //Collision-
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
        ResourceManager.GetInstance().checkAndBuyGold(GoldCost);
        Obstacle.enabled = true;
    }
    public virtual void BuildThis()
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
        if (TimeLeft >= TikTimer) NewTik();
    }

    public virtual void NewTik()
    {
        TimeLeft = 0;
    }

    public virtual void UpgradeThis()
    {
        Level += 1;
    }

    public void SetCost(int Cost)
    {
        GoldCost = Cost;
    }
}