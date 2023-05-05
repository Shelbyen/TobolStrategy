using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class BuildingButton : MonoBehaviour
{
    public GameObject Building;
    public int Cost;
    public int FreeBuildCount;
    public int MaxCount;
    private int Count;
    private GameObject FreeBuild;
    private TMP_Text FreeBuildText;

    private Builder Builder;

    void Awake()
    {
        Builder = GameObject.Find("Builder").GetComponent<Builder>();
        FreeBuild = transform.GetChild(0).gameObject;
        FreeBuildText = GetComponentInChildren<TMP_Text>();

        if (FreeBuildCount > 0) FreeBuild.SetActive(true);
        else FreeBuild.SetActive(false);
        FreeBuildText.text = $"{"x" + FreeBuildCount}";
    }

    void Update()
    {
        
    }

    public void StructureBuilt()
    {
        FreeBuildCount -= 1;
        Count += 1;
        if (FreeBuildCount > 0) FreeBuild.SetActive(true);
        else FreeBuild.SetActive(false);
        FreeBuildText.text = $"{"x" + FreeBuildCount}";
        if (Count >= MaxCount) GetComponent<Button>().interactable = false;
    }

    public void SpawnBuilding()
    {
        if (FreeBuildCount > 0) Builder.StartBuilding(Building, 0, GetComponent<BuildingButton>());
        else Builder.StartBuilding(Building, Cost, GetComponent<BuildingButton>());
    }
}
