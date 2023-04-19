using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBuilding : MonoBehaviour
{
    public float HealthPerHeal;
    public float HealPeriod = 1;

    public void StartHeal()
    {
        StartCoroutine(Heal());
    }

    public IEnumerator Heal()
    {
        while (true)
        {
            if (HealPeriod == 0) HealPeriod = 1;
            yield return new WaitForSeconds(HealPeriod);
            GameObject[] HumansForHeal = GameObject.FindGameObjectsWithTag("Human");
            foreach (GameObject human in HumansForHeal)
            {
                if (human.GetComponent<Human>().HP <= human.GetComponent<Human>().MaxHP * 0.75)
                {
                    human.GetComponent<Human>().HP += HealthPerHeal;
                    Debug.Log("HospitalHeal: " + human.GetComponent<Human>().HP);
                }
            }
        }
    }
}
