using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Building : MonoBehaviour
{
    [SerializeField] protected BuildingData BuildingData;
    [SerializeField] protected int Level;
    [NonSerialized] protected bool Placed;
    [NonSerialized] protected bool Built;
    [NonSerialized] protected float BuildProgress;
    [SerializeField] protected float TimeLeft;

    protected int GoldCost;
    protected NavMeshObstacle Obstacle;
    protected int CollisionCount;
    protected Material[] BaseMaterial;
    protected Renderer[] Render;

    protected virtual void Awake()
    {
        Obstacle = GetComponent<NavMeshObstacle>();
        Obstacle.enabled = false;
        Render = GetComponentsInChildren<Renderer>();
        BaseMaterial = new Material[Render.Length];
        for (int i = 0; i < Render.Length; i += 1)
        {
            BaseMaterial[i] = Render[i].material;
        }
        CheckPlace();
    }

    protected void Update()
    {
        if (!Built)
        {
            if (Placed) BuildProgress += 10 * Time.deltaTime;
            if (BuildProgress >= 100) BuildThis();
        }
        else
        {
            if (BuildingData.TikTimer != 0) Timer();
        }
    }

    protected virtual void OnDestroy()
    {
        if (Placed && !Built) ResourceManager.GetInstance().addGold(GoldCost);
    }

    protected void OnTriggerEnter()
    {
        if (!Placed) CollisionCount += 1;
    } //Collision+
    protected void OnTriggerExit()
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
        Render = new Renderer[0];
        BaseMaterial = new Material[0];
    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    protected void Timer()
    {
        TimeLeft += Time.deltaTime;
        if (TimeLeft >= BuildingData.TikTimer) NewTik();
    }

    protected virtual void NewTik()
    {
        TimeLeft = 0;
    }

    public float TimeLeftPercent()
    {
        if (BuildingData.TikTimer != 0)
        {
            return TimeLeft / BuildingData.TikTimer;
        }
        else
        {
            return 1f;
        }
    }

    public virtual void UpgradeThis()
    {
        ResourceManager.GetInstance().checkAndBuyGold(BuildingData.UpgradeCost[Level]);
        Level += 1;
    }

    public void SetCost(int Cost)
    {
        GoldCost = Cost;
    }

    public virtual void AddUnit()
    {

    }

    public virtual void DeleteUnit()
    {

    }

    public virtual void ShowStats()
    {
        LinkManager.GetUIManager().MainStats.SetSprite(BuildingData.BuildingTypeImage);
        LinkManager.GetUIManager().MainStats.SetType(BuildingData.Name);
        LinkManager.GetUIManager().MainStats.SetLevel(Level + 1);
        if (BuildingData.UpgradeCost.Length > Level) 
            LinkManager.GetUIManager().MainStats.SetUpgradeCost(BuildingData.UpgradeCost[Level].ToString());
        else LinkManager.GetUIManager().MainStats.SetUpgradeCost("---");

        LinkManager.GetUIManager().MainStats.SetWindowStatus(true);
    }

    public virtual void ShowUnits()
    {
        LinkManager.GetUIManager().MainStats.SetUnitsInfo("no vacancies", "");
    }

    public virtual void ShowUpdatingStats()
    {
        LinkManager.GetUIManager().MainStats.SetHealth(BuildProgress);
        LinkManager.GetUIManager().MainStats.SetTimer(TimeLeftPercent());
        CheckUpgrade();
        CheckVacancies();
        ShowUnits();
    }

    public virtual void CheckVacancies()
    {
        LinkManager.GetUIManager().MainStats.HireUnits(false);
        LinkManager.GetUIManager().MainStats.FireUnits(false);
        LinkManager.GetUIManager().MainStats.SetUnitCost(0);
    }

    public virtual void CheckUpgrade()
    {
        if (Built && Level < ResourceManager.GetInstance().checkMaxLv() && Level < BuildingData.UpgradeCost.Length && ResourceManager.GetInstance().checkGold(BuildingData.UpgradeCost[Level]))
        {
            LinkManager.GetUIManager().MainStats.SetUpgrade(true);
        }
        else LinkManager.GetUIManager().MainStats.SetUpgrade(false);
    }
}