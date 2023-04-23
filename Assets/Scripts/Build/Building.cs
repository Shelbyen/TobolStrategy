using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class Building : MonoBehaviour
{
    public string Discription;

    public bool IsEnemy; 
    public bool BuildOnStart;

    public bool IsWrongPlace;
    public bool Placed;
    public bool Built;

    public float BuildProgress;
    public int GoldCost;
    public int GoldMining;

    public GameObject Unit;
    public int UnitNumber;
    public GameObject Enter;

    //public GameObject StatusWindow;
    //public Vector3 StatusWindowPosition;

    private NavMeshObstacle Obstacle;
    private Builder BuilderScript;
    private int CollisionCount;
    private Material[] BaseMaterial;
    private Renderer[] Render;
    private Collider BuildingCollider;
    private GameObject[] BuildingsUnits;

    private bool SpawningUnitsNow;

    void Awake()
    {
        Obstacle = GetComponent<NavMeshObstacle>();
        Obstacle.enabled = false;
        BuilderScript = GameObject.Find("Builder").GetComponent<Builder>();
        BuildingCollider = GetComponent<Collider>();
        Render = GetComponentsInChildren<Renderer>();
        BaseMaterial = new Material[Render.Length];
        BuildingsUnits = new GameObject[UnitNumber];
        for (int i = 0; i < Render.Length; i += 1)
        {
            BaseMaterial[i] = Render[i].material;
        }
        if (BuildOnStart) BuildThis();
        else
        {
            if (GoldCost <= ResourceManager.GetInstance().getCountGold() && CollisionCount <= 0) GoodPlace();
            else WrongPlace();

            if (!Placed)
            {
                foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) renderer.material = BuilderScript.GoodMaterial;
            }
        }
        //StatusWindow.transform.SetParent(GameObject.Find("Worldspace Canvas").transform);
        //StatusWindow.SetActive(false);
        //StatusWindow.GetComponentInChildren<TMP_Text>().text = $"{GetComponent<Selectable>().Name + " Lv1"}";
    }

    void FixedUpdate()
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

    void OnDestroy()
    {
        if (!Built) ResourceManager.GetInstance().addGold(GoldCost);
        KillAll();
    }

    private void OnTriggerEnter(Collider Collider)
    {
        if (!Placed) CollisionCount += 1;
    }
    private void OnTriggerExit(Collider Collider)
    {
        if (!Placed) CollisionCount -= 1;
    }

    public void WrongPlace()
    {
        Debug.Log("Good place or No money");
        IsWrongPlace = true;
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) renderer.material = BuilderScript.WrongMaterial;
    }

    public void GoodPlace()
    {
        Debug.Log("All is good");
        IsWrongPlace = false;
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) renderer.material = BuilderScript.GoodMaterial;
    }

    public void PlaceThis()
    {
        Placed = true;
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) renderer.material = BuilderScript.GoodMaterial;
        Obstacle.enabled = true;
        //StatusWindow.transform.position = transform.position + StatusWindowPosition;
    }

    public void BuildThis()
    {
        Obstacle.enabled = true;
        Placed = true;
        Built = true;
        for (int i = 0; i < Render.Length; i += 1) Render[i].material = BaseMaterial[i];
        if (GoldMining != 0) StartCoroutine(GoldMine());
        if (UnitNumber != 0 && !IsEnemy) StartCoroutine(SpawnUnits());
        if (GetComponent<HealBuilding>()) GetComponent<HealBuilding>().StartHeal();
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    public IEnumerator SpawnUnits()
    {
        if (!SpawningUnitsNow)
        {
            SpawningUnitsNow = true;
            KillAll();
            for (int i = 0; i < UnitNumber; i += 1)
            {
                BuildingsUnits[i] = Instantiate(Unit);
                BuildingsUnits[i].transform.position = Enter.transform.position;
                BuildingsUnits[i].GetComponent<Human>().Target = Enter.transform.position;
                if (IsEnemy)
                {
                    BuildingsUnits[i].tag = "Enemy";
                }
                else
                {
                    BuildingsUnits[i].tag = "Human";
                }
                yield return new WaitForSeconds(0.5f);
            }
            SpawningUnitsNow = false;
        }
    }

    public void KillAll()
    {
        foreach(GameObject UnitForKill in BuildingsUnits) Destroy(UnitForKill);
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