using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool IsWrongPlace;
    public bool Placed;
    public bool Built;
    public bool Destruction;

    public float HP;
    public Vector3 Enter;

    public int CollisionCount;

    public void OnTriggerEnter(Collider Collider)
    {
        CollisionCount += 1;
        WrongPlace();
    }
    public void OnTriggerExit(Collider Collider)
    {
        CollisionCount -= 1;
        if (CollisionCount == 0) GoodPlace();
    }

    public void WrongPlace()
    {
        Debug.Log("Wrong place");
        IsWrongPlace = true;
    }

    public void GoodPlace()
    {
        Debug.Log("Good place");
        IsWrongPlace = false;
    }
}
