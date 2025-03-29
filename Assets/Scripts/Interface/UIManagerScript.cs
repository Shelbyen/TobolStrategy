using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIManagerScript : MonoBehaviour
{
    //Resources
    [SerializeField] private TMP_Text GoldCount;
    [SerializeField] private TMP_Text Population;
    [SerializeField] private TMP_Text Faith;

    [SerializeField] private GameObject BuildMenu;
    [SerializeField] private GameObject GoldCost;
    [SerializeField] private TMP_Text RaidStatus;

    [SerializeField] private Button[] BuildPages;

    public MainBuildingStats MainStats;
    public HealStats HealStats;
    public MiningStats MiningStats;
    public SummonStats SummonStats;

    private void Awake()
    {
        BuildMenu.SetActive(false);
        GoldCost.SetActive(false);
        GoldCost.GetComponentsInChildren<TMP_Text>()[0].text = "";

        foreach (Button Page in BuildPages)
        {
            Page.interactable = false;
        }
    }

    public void CloseStatWindows()
    {
        MainStats.SetWindowStatus(false);
        HealStats.SetWindowStatus(false);
        MiningStats.SetWindowStatus(false);
        SummonStats.SetWindowStatus(false);
    }

    public void OpenPage(int Page)
    {
        BuildPages[Page].interactable = true;
    }
    
    private void Update() 
    {
        ShowResourceData();
    }

    public void ShowResourceData()
    {
        GoldCount.text = ResourceManager.GetInstance().getCountGold().ToString();        
        Population.text = $"{(ResourceManager.GetInstance().maxHumansCount() - ResourceManager.GetInstance().usedHumansCount()) + "/" + ResourceManager.GetInstance().maxHumansCount()}";
        Faith.text = ResourceManager.GetInstance().getFaith().ToString();
    }

    public void ChangeStatusGoldCost (bool State) {
        GoldCost.SetActive(State);
    }

    public void ChangeStatusBuildMenu (bool State) {
        BuildMenu.SetActive(State);
    }

    public void ChangeTextRaidStatus(string newText)
    {
        RaidStatus.text = newText;
    }

    public void ChangeTextGoldCost(string newText) {
        GoldCost.GetComponentsInChildren<TMP_Text>()[0].text = newText;
    }
}
