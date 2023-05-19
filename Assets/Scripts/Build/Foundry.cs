using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundry : Building
{
    public GameObject Bullet;
    public GameObject CannonBall;

    public int[] BulletDamage;
    public int[] CannonBallDamage;

    public int DefaultBulletDamage;
    public int DefaultBallDamage;

    public override void BuildThis()
    {
        base.BuildThis();
        Bullet.GetComponent<BulletController>().damage = BulletDamage[Level];
        //CannonBall.GetComponent<BulletController>().damage = CannonBallDamage[Level];
    }

    public override void UpgradeThis()
    {
        base.UpgradeThis();
        Bullet.GetComponent<BulletController>().damage = BulletDamage[Level];
        //CannonBall.GetComponent<BulletController>().damage = CannonBallDamage[Level];
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        Bullet.GetComponent<BulletController>().damage = DefaultBulletDamage;
        //CannonBall.GetComponent<BulletController>().damage = DefaultBallDamage;
    }
}
