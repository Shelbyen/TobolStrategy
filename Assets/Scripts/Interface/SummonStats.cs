using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SummonStats : WindowStats
{
    [SerializeField] private TMP_Text UnitName;
    [SerializeField] private TMP_Text UnitHealth;
    [SerializeField] private TMP_Text UnitDamage;
    [SerializeField] private TMP_Text UnitSpeed;

    public void SetName(string name)
    {
        UnitName.text = Localization.ReturnTranslation(name);
    }
    public void SetHealth(float hp)
    {
        UnitHealth.text = Localization.ReturnTranslation("unit health") + " " + hp;
    }
    public void SetDamage(float damage)
    {
        UnitDamage.text = Localization.ReturnTranslation("unit damage") + " " + damage;
    }
    public void SetSpeed(float speed)
    {
        UnitSpeed.text = Localization.ReturnTranslation("unit speed") + " " + speed;
    }
}
