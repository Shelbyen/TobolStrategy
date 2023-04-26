using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBuilding : MonoBehaviour
{
    public float[] HealthPerHeal;

    public void Heal(int Lv)
    {
        GameObject[] HumansForHeal = GameObject.FindGameObjectsWithTag("Human");
        foreach (GameObject human in HumansForHeal)
        {
            if (human.GetComponent<Human>().HP <= human.GetComponent<Human>().MaxHP * 0.75)
            {
                human.GetComponent<Human>().HP += HealthPerHeal[Lv];
                Debug.Log("HospitalHeal: " + human.GetComponent<Human>().HP);
            }
        }
    }
}
