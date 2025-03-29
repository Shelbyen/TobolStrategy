using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundry : Building
{
    [SerializeField] protected FoundryData FoundryData;
    
    public override void BuildThis()
    {
        base.BuildThis();
        FoundryData.Bullet.GetComponent<BulletController>().damage = FoundryData.BulletDamage[Level];
        //FoundryData.CannonBall.GetComponent<BulletController>().damage = FoundryData.CannonBallDamage[Level];
    }

    public override void UpgradeThis()
    {
        base.UpgradeThis();
        FoundryData.Bullet.GetComponent<BulletController>().damage = FoundryData.BulletDamage[Level];
        //FoundryData.CannonBall.GetComponent<BulletController>().damage = FoundryData.CannonBallDamage[Level];
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        FoundryData.Bullet.GetComponent<BulletController>().damage = FoundryData.DefaultBulletDamage;
        //FoundryData.CannonBall.GetComponent<BulletController>().damage = FoundryData.DefaultBallDamage;
    }
}
