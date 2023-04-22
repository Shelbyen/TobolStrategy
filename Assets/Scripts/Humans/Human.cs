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
    public LayerMask layer;

    private RaycastHit Hit;
    private Camera MainCamera;
    public bool isSelect;
    public bool Attack = false;

    public bool CanShoot;
    public float meleeDamage;
    public GameObject bullet;
    private Coroutine _coroutine = null;

    private AudioSource audioSrc;

    public void Awake() 
    {
        HP = MaxHP;
        MainCamera = Camera.main;
        TargetEnemy = null;
        Target = gameObject.transform.position;
        audioSrc = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (HP <= 0) Destroy(gameObject);
        if (transform.tag == "Human") UpdateHealthBar();
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
                Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                Debug.Log(ray);
                /*S
                if (Physics.Raycast(ray, out Hit, 1000f, 512))
                {
                    if (Hit.transform.gameObject.tag == "Enemy")
                    {
                        TargetEnemy = Hit.transform.gameObject;
                    }
                }
                */
                if (Physics.Raycast(ray, out Hit, 1000f, layer))
                {
                    GetComponent<NavMeshAgent>().SetDestination(Hit.point);
                    Debug.Log(Hit.point);
                }
            }
        }
    }

    private void FindTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, AttackRange);
        int enemyCount = 0;

        foreach (var el in hitColliders)
        {
            if ((gameObject.CompareTag("Human") && el.gameObject.CompareTag("Enemy")) ||
                (gameObject.CompareTag("Enemy") && el.gameObject.CompareTag("Human")))
            {
                enemyCount += 1;

                if (!Attack)
                {
                    Attack = true;
                    GetComponent<NavMeshAgent>().SetDestination(transform.position);
                }
                
                if (_coroutine == null)
                {
                    _coroutine = StartCoroutine(StartAttack(el));
                }
            }
        }
        if (enemyCount == 0 && _coroutine != null)
        {
            Attack = false;
            StopCoroutine(_coroutine);
            _coroutine = null;
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
        if (CanShoot)
        {
            GameObject obj = Instantiate(bullet, transform.GetChild(1).position, Quaternion.identity);
            obj.GetComponent<BulletController>().position = enemy.transform.position;
        }
        else
        {
            enemy.GetComponent<Human>().HP -= meleeDamage;
        }
        yield return new WaitForSeconds(1f);
        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}
