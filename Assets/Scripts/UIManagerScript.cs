using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIManagerScript : MonoBehaviour
{
    //Resources
    public TMP_Text GoldCount;
    public TMP_Text Population;
    public TMP_Text Faith;

    public GameObject BuildMenu;
    public Image ToggleImage;
    public Image GridImage;
    public Image DestroyImage;
    public GameObject GoldCost;
    public TMP_Text RaidStatus;
    //Sprites
    public Sprite BuildSprite;
    public Sprite ViewSprite;
    public Sprite GridDefault;
    public Sprite GridPressed;
    public Sprite DestroyDefault;
    public Sprite DestroyPressed;

    public Button[] BuildPages;

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

    public void ChangeBuildToggleImage(bool Status) {
        if (Status) ToggleImage.sprite = BuildSprite;
        else ToggleImage.sprite = ViewSprite;
    }

    public void ChangeGridImage(bool Status)
    {
        if (Status) GridImage.sprite = GridPressed;
        else GridImage.sprite = GridDefault;
    }

    public void ChangeDestroyImage(bool Status)
    {
        if (Status) DestroyImage.sprite = DestroyPressed;
        else DestroyImage.sprite = DestroyDefault;
    }

    public void ChangeTextRaidStatus(string newText)
    {
        RaidStatus.text = newText;
    }

    public void ChangeTextGoldCost(string newText) {
        GoldCost.GetComponentsInChildren<TMP_Text>()[0].text = newText;
    }
}
