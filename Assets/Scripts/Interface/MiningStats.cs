using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiningStats : WindowStats
{
    [SerializeField] private TMP_Text GoldMining;

    public void SetMining(int gold)
    {
        GoldMining.text = Localization.ReturnTranslation("mining") + " " + gold.ToString();
    }
}
