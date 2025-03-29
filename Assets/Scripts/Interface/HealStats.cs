using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class HealStats : WindowStats
{
    [SerializeField] private TMP_Text HealPerTik;
    [SerializeField] private TMP_Text MaxHealth;

    public void SetHealPerTik(float heal)
    {
        HealPerTik.text = Localization.ReturnTranslation("heal tik") + " " + heal.ToString();
    }
    public void SetMaxHealth(float health)
    {
        MaxHealth.text = Localization.ReturnTranslation("max health") + " " + health.ToString() + "%";
    }
}
