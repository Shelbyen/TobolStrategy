using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour
{
    public float MaxHP;
    public float HP;
    public float MaxView;
    public float AttackRange;
    public Vector3 Target;
    public GameObject TargetEnemy;
    public bool IsEnemy;

    private RaycastHit Hit;
    private Camera MainCamera;
    private NavMeshAgent Agent;
    public bool isSelect;
    public bool Attack = false;

    public bool CanShoot;
    public Collider AtackCollider;
    public float AtackForce;
    public GameObject bullet;
    private Coroutine _coroutine;

    private AudioSource audioSrc;

    public void Awake() 
    {
        HP = MaxHP;
        Agent = GetComponent<NavMeshAgent>();
        MainCamera = Camera.main;
        TargetEnemy = null;
        Target = gameObject.transform.position;
        audioSrc = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (HP <= 0) Destroy(gameObject);
        UpdateHealthBar();
        SetTarget();

        FindTarget();
        if (!Attack)
        {
            FindEnemy();
        }
    }

    private void UpdateHealthBar()
    {
        Transform healthBar = transform.GetChild(0).transform;
        healthBar.localScale = new Vector3(
            (healthBar.localScale.x / MaxHP) * HP,
            healthBar.localScale.y,
            healthBar.localScale.z);
    }

    public void SetTarget()
    {
        if (isSelect)
        {
            if (InputManager.GetKeyDown("Place"))
            {
                if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out Hit, 1000f, 512))
                {
                    if (Hit.transform.gameObject.tag == "Enemy")
                    {
                        TargetEnemy = Hit.transform.gameObject;
                    }
                }
                else if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out Hit, 1000f, 8))
                {
                    Target = Hit.point;
                    TargetEnemy = null;
                }
            }
        }
    }

    private void FindTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, AttackRange);
        int enemyCount = 0;

        if (hitColliders.Length != 0 && !Attack)
        {
            Attack = true;
        }


        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        else if (hitColliders.Length == 0)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
            Attack = false;
        }

        foreach (var el in hitColliders)
        {
            if ((gameObject.CompareTag("Human") && el.gameObject.CompareTag("Enemy")) ||
                (gameObject.CompareTag("Enemy") && el.gameObject.CompareTag("Human")))
            {
                enemyCount += 1;
                Debug.Log(transform.position.x - el.transform.position.x);
                if (_coroutine == null)
                {
                    _coroutine = StartCoroutine(StartAttack(el));
                }
            }
        }
        if (enemyCount > 0 && !Attack)
        {
            Attack = true;
            GetComponent<NavMeshAgent>().SetDestination(transform.position);
        }
        else if (enemyCount == 0)
        {

        }
    }

    public void FindEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, MaxView);
        
        foreach (var el in hitColliders)
        {
            if ((gameObject.CompareTag("Human") && el.gameObject.CompareTag("Enemy"))||
                (gameObject.CompareTag("Enemy") && el.gameObject.CompareTag("Human")))
            {
                GetComponent<NavMeshAgent>().SetDestination(el.transform.position);
            }
        }

        /*
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
        */
    }

    IEnumerator StartAttack(Collider enemy)
    {
        GameObject obj = Instantiate(bullet, transform.GetChild(1).position, Quaternion.identity);
        obj.GetComponent<BulletController>().position = enemy.transform.position;
        yield return new WaitForSeconds(1f);
        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}
