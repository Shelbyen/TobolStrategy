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

    void OnColliderStay()
    {

    }

    public void WrongPlace()
    {
        IsWrongPlace = true;
    }

    public void GoodPlace()
    {
        IsWrongPlace = false;
    }
}
