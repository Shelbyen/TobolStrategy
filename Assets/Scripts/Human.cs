using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour
{
    public float HP;
    public Vector3 Target;
    public GameObject TargetEnemy;
    public bool IsEnemy;

    private Selectable SelectableScript;
    private RaycastHit Hit;
    private Camera MainCamera;
    private NavMeshAgent Agent;

    public Collider AtackCollider;
    public float AtackForce;

    public void Awake() 
    {
        Target = transform.position;
        Agent = GetComponent<NavMeshAgent>();
        MainCamera = Camera.main;
        SelectableScript = GetComponent<Selectable>();
    }

    public void Update()
    {
        if (SelectableScript.Selected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out Hit, 100f, 512))
                {
                    if (Hit.transform.gameObject.GetComponent<Human>())
                    {
                        TargetEnemy = Hit.transform.gameObject;
                    }
                }
                else if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out Hit, 100f, 64))
                {
                    Target = Hit.point;
                    TargetEnemy = null;
                }
            }
        }

        if (TargetEnemy != null)
        {
            Target = TargetEnemy.transform.position;
        }
        FindEnemy();
    }
    public void FixedUpdate()
    { 
        Agent.SetDestination(Target);

        if (HP <= 0) Destroy(gameObject);
    }

    public void OnTriggerStay(Collider Collider)
    {
        if (Collider.gameObject == TargetEnemy) Collider.gameObject.GetComponent<Human>().HP -= AtackForce;
    }

    public void FindEnemy()
    {
        GameObject[] EnemyList = GameObject.FindGameObjectsWithTag("Human");

        Vector3 EnemyDistance = new Vector3(10000, 10000, 10000);
        int Enemy = -1;

        for (int x = 0; x < EnemyList.Length; x += 1)
        {
            if (EnemyList[x] != gameObject && EnemyList[x].GetComponent<Human>().IsEnemy != IsEnemy)
            { 
                Vector3 dist = EnemyList[x].transform.position - transform.position;
                if ((dist.x + dist.z) < (EnemyDistance.x + EnemyDistance.z))
                {
                    EnemyDistance = dist;
                    Enemy = x;
                }
            }
        }
        if (Enemy != -1) TargetEnemy = EnemyList[Enemy].gameObject;
    }
}
