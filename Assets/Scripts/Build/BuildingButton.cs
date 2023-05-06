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
    public List<GameObject> PlacedBuildings;
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

    void FixedUpdate()
    {
        for (int i = 0; i < PlacedBuildings.Count; i += 1)
        {
            if (PlacedBuildings[i] == null)
            {
                PlacedBuildings.RemoveAt(i);
                if (PlacedBuildings.Count < MaxCount) GetComponent<Button>().interactable = true;
            }
        }
    }

    public void StructureBuilt(GameObject Placed)
    {
        PlacedBuildings.Add(Placed);
        if (FreeBuildCount > 0)
        {
            FreeBuildCount -= 1;
            FreeBuild.SetActive(true);
        }
        if (FreeBuildCount <= 0)  FreeBuild.SetActive(false);
        FreeBuildText.text = $"{"x" + FreeBuildCount}";
        if (PlacedBuildings.Count >= MaxCount) GetComponent<Button>().interactable = false;
    }

    public void SpawnBuilding()
    {
        if (PlacedBuildings.Count < MaxCount)
        {
            if (FreeBuildCount > 0) Builder.StartBuilding(Building, 0, GetComponent<BuildingButton>());
            else Builder.StartBuilding(Building, Cost, GetComponent<BuildingButton>());
        }
    }
}
