using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIManagerScript : MonoBehaviour
{
    public TMP_Text GoldCount;
    public GameObject BuildMenu;
    public TMP_Text StatusBar;
    public Image ToggleImage;
    public Image GridImage;
    public Image DestroyImage;
    public GameObject GoldCost;
    public TMP_Text RaidStatus;

    public Sprite BuildSprite;
    public Sprite ViewSprite;

    public Sprite GridDefault;
    public Sprite GridPressed;

    public Sprite DestroyDefault;
    public Sprite DestroyPressed;

    private void Awake() {
        BuildMenu.SetActive(false);
        GoldCost.SetActive(false);
        GoldCost.GetComponentsInChildren<TMP_Text>()[0].text = "";
    }
    
    private void Update() {
        if (ResourceManager.GetInstance().getCountGold() <= 9999) GoldCount.text = ResourceManager.GetInstance().getCountGold().ToString();
        else GoldCount.text = "9999+";
    }

    public void ChangeStatusGoldCost (bool State) {
        GoldCost.SetActive(State);
    }

    public void ChangeStatusBuildMenu (bool State) {
        BuildMenu.SetActive(State);
    }

    public void ChangeTextStatusBar(string newText) {
        StatusBar.text = newText;
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
