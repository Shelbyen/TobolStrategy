using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour
{
    public float MaxHP;
    public float HP;
    public float MaxView;
    public Vector3 Target;
    public GameObject TargetEnemy;
    public bool IsEnemy;

    private Selectable SelectableScript;
    private RaycastHit Hit;
    private Camera MainCamera;
    private NavMeshAgent Agent;
    public bool Shooting;

    public bool CanShoot;
    public Collider AtackCollider;
    public float AtackForce;

    private AudioSource audioSrc;

    public void Awake() 
    {
        HP = MaxHP;
        Agent = GetComponent<NavMeshAgent>();
        MainCamera = Camera.main;
        SelectableScript = GetComponent<Selectable>();
        TargetEnemy = null;
        Target = gameObject.transform.position;
        audioSrc = GetComponent<AudioSource>();
    }

    public void Update()
    {
        SetTarget();

        if (TargetEnemy != null && !Shooting)
        {
            Target = TargetEnemy.transform.position;
        }
        FindEnemy();
    }

    public void SetTarget()
    {
        if (SelectableScript.Selected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out Hit, 1000f, 512))
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
    }

    public void FixedUpdate()
    {
        if (!Shooting)
        {

                Agent.SetDestination(Target);
            
        }
        if (HP <= 0) Destroy(gameObject);
    }

    public void OnTriggerStay(Collider Collider)
    {
        if (Collider.gameObject == TargetEnemy && Collider.isTrigger)
        {
            if (CanShoot)
            {
                Shooting = true;
                Agent.SetDestination(transform.position);
                audioSrc.Play();
            }
            Collider.gameObject.GetComponent<Human>().HP -= AtackForce;
        }
        Shooting = false;
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
                if (Mathf.Sqrt(dist.x * dist.x + dist.z * dist.z) < Mathf.Sqrt(EnemyDistance.x * EnemyDistance.x + EnemyDistance.z * EnemyDistance.z))
                {
                    EnemyDistance = dist;
                    Enemy = x;
                   
                }
            }
        }
        if(Enemy != -1 && Mathf.Sqrt(EnemyDistance.x * EnemyDistance.x + EnemyDistance.z * EnemyDistance.z) <= MaxView) TargetEnemy = EnemyList[Enemy].gameObject;
    }
}
