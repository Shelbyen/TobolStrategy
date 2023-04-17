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
    public TMP_Text ToggleText;
    public GameObject GoldCost;

    private void Awake() {
        BuildMenu.SetActive(false);
        GoldCost.SetActive(false);
        GoldCost.GetComponentsInChildren<TMP_Text>()[0].text = "";
    }
    
    private void Update() {
        GoldCount.text = ResourceManager.GetInstance().getCountGold().ToString();
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

    public void ChangeTextToggleText(string newText = "") {
        ToggleText.text = newText;
    }

    public void ChangeTextGoldCost(string newText) {
        GoldCost.GetComponentsInChildren<TMP_Text>()[0].text = newText;
    }
}
