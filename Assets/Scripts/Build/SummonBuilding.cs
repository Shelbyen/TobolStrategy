using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System.Linq;

public class SummonBuilding : Building
{
    public GameObject Unit;
    public int[] MaxUnitNumber;
    public GameObject Enter; //Вектор3

    public int[] UnitCost;
    public float[] UnitDamage;
    public float[] UnitMaxHP;
    public float[] UnitSpeed;
    public List<GameObject> BuildingsUnits;

    void FixedUpdate()
    {
        CheckUnits();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        KillAll();
    }

    public override void UpgradeThis()
    {
        base.UpgradeThis();
        UpgradeUnits();
    }

        public void UpgradeUnits()
    {
        foreach (GameObject Unit in BuildingsUnits)
        {
            Unit.GetComponent<Human>().MaxHP = UnitMaxHP[Level];
            Unit.GetComponent<Human>().meleeDamage = UnitDamage[Level];
            Unit.GetComponent<NavMeshAgent>().speed = UnitSpeed[Level];
        }
    }

    public void BuyUnit()
    {
        ResourceManager.GetInstance().useHuman(1);
        ResourceManager.GetInstance().checkAndBuyGold(UnitCost[Level]);
        BuildingsUnits.Add(Instantiate(Unit, Enter.transform.position, Quaternion.identity));
        BuildingsUnits.Last().GetComponent<Human>().Target = Enter.transform.position;
        BuildingsUnits.Last().GetComponent<Human>().Summon = GetComponent<SummonBuilding>();
        BuildingsUnits.Last().GetComponent<Human>().UpdateLevelData(Level);
        BuildingsUnits.Last().GetComponent<Human>().HP = UnitMaxHP[Level];
    }

    public void DeleteUnit()
    {
        Destroy(BuildingsUnits.Last());
        BuildingsUnits.RemoveAt(BuildingsUnits.Count - 1);
    }

    public void CheckUnits()
    {
        for (int i = 0; i < BuildingsUnits.Count; i += 1)
        {
            if (BuildingsUnits[i] == null)
            {
                BuildingsUnits.RemoveAt(i);
            }
        }
    }

    public void KillAll()
    {
        foreach (GameObject UnitForKill in BuildingsUnits)
        {
            Destroy(UnitForKill);
        }
    }
}
