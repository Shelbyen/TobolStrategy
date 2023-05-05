using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SummonBuilding : MonoBehaviour
{
    private Building Building;

    public GameObject Unit;
    public int[] MaxUnitNumber;
    public GameObject Enter; //Вектор3

    public int[] UnitCost;
    public float[] UnitDamage;
    public float[] UnitMaxHP;
    public float[] UnitSpeed;
    public List<GameObject> BuildingsUnits;

    void Awake()
    {
        Building = GetComponent<Building>();
    }

    void OnDestroy()
    {
        KillAll();
    }

    public void UpgradeUnits()
    {
        foreach (GameObject Unit in BuildingsUnits)
        {
            Unit.GetComponent<Human>().UpdateLevelData(Building.Level);
        }
    }

    public void BuyUnit()
    {
        ResourceManager.GetInstance().useHuman(1);
        ResourceManager.GetInstance().checkAndBuyGold(UnitCost[Building.Level]);
        BuildingsUnits.Add(Instantiate(Unit, Enter.transform.position, Quaternion.identity));
        BuildingsUnits.Last().GetComponent<Human>().Target = Enter.transform.position;
        BuildingsUnits.Last().GetComponent<Human>().Summon = GetComponent<SummonBuilding>();
        BuildingsUnits.Last().GetComponent<Human>().UpdateLevelData(Building.Level);
        BuildingsUnits.Last().GetComponent<Human>().HP = UnitMaxHP[Building.Level];
    }

    public void DeleteUnit()
    {
        Destroy(BuildingsUnits.Last());
        BuildingsUnits.RemoveAt(BuildingsUnits.Count - 1);
    }

    public void CheckUnits(GameObject Unit)
    {
        for (int i = 0; i < BuildingsUnits.Count; i += 1)
        {
            if (BuildingsUnits[i] == Unit)
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
