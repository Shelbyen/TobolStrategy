using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoundryData", menuName = "BuildingData/Foundry", order = 1)]
public class FoundryData : ScriptableObject
{
    public GameObject Bullet;
    public GameObject CannonBall;
    public int[] BulletDamage;
    public int[] CannonBallDamage;
    public int DefaultBulletDamage;
    public int DefaultBallDamage;
}
