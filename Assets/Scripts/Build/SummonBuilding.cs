using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SummonBuilding : MonoBehaviour
{
    private Building Building;

    public GameObject Unit;
    public int[] MaxUnitNumber;
    public GameObject Enter; //В Вектор3

    public int[] UnitCost;

    public float[] UnitDamage;
    public float[] UnitMaxHP;
    public float[] UnitSpeed;
    public int[] BulletDamage;
    public GameObject UnitBullet;

    public List<GameObject> BuildingsUnits;

    void Awake()
    {
        Building = GetComponent<Building>();
        if (UnitBullet != null) UnitBullet.GetComponent<BulletController>().damage = BulletDamage[Building.Level];
    }

    void OnDestroy()
    {
        KillAll();
    }

    public void Upgrade()
    {
        if (UnitBullet != null) UnitBullet.GetComponent<BulletController>().damage = BulletDamage[Building.Level];
        foreach (GameObject Unit in BuildingsUnits)
        {
            Unit.GetComponent<Human>().UpdateLevelData(Building.Level);
        }
    }

    public void BuyUnit()
    {
        ResourceManager.GetInstance().checkAndBuyGold(UnitCost[Building.Level]);
        BuildingsUnits.Add(Instantiate(Unit));
        BuildingsUnits.Last().transform.position = Enter.transform.position;
        BuildingsUnits.Last().GetComponent<Human>().Target = Enter.transform.position;
        BuildingsUnits.Last().GetComponent<Human>().Summon = GetComponent<SummonBuilding>();
        BuildingsUnits.Last().GetComponent<Human>().UpdateLevelData(Building.Level);
        BuildingsUnits.Last().GetComponent<Human>().HP = UnitMaxHP[Building.Level];
    }

    public void KillAll()
    {
        foreach (GameObject UnitForKill in BuildingsUnits) Destroy(UnitForKill);
    }
}
