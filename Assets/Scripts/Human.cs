using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Human : MonoBehaviour
{
    public float MaxHP;
    public float HP;
    public float MaxView;
    public float AttackRange;
    public Vector3 Target;
    
    public LayerMask layer;

    private RaycastHit Hit;
    private Camera MainCamera;
    public bool isSelect;
    public bool Attack = false;
    public GameObject _priorityEnemy = null;

    public bool CanShoot;
    public float meleeDamage;
    public GameObject bullet;
    private Coroutine _coroutine = null;

    private Animator animator;
    private NavMeshAgent agent;
    private float _maxHealthBarScale;
    private AudioSource audioSrc;

    public SummonBuilding Summon;

    void Awake() 
    {
        HP = MaxHP;
        MainCamera = Camera.main;
        audioSrc = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(Target);

        if (transform.tag == "Human") _maxHealthBarScale = transform.GetChild(0).transform.localScale.x;
    }

    void OnDestroy()
    {
        if (transform.tag == "Human")
        {
            ResourceManager.GetInstance().useHuman(-1);
        }
    }

    public void UpdateLevelData(int Lv)
    {
        MaxHP = Summon.SummonData.UnitMaxHP[Lv];
        meleeDamage = Summon.SummonData.UnitDamage[Lv];
        agent.speed = Summon.SummonData.UnitSpeed[Lv];
    }

    public void Update()
    {
            if (transform.tag == "Human") UpdateHealthBar();
        SetTarget();

        FindPriorityEnemy();

        if (!Attack && _priorityEnemy.gameObject != null)
        {
            agent.SetDestination(_priorityEnemy.transform.position);
        }
        else if (!Attack)
        {
            if (agent.velocity != Vector3.zero)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        AttackTarget();

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        Transform healthBar = transform.GetChild(0).transform;
        healthBar.localScale = new Vector3(
            _maxHealthBarScale * HP / MaxHP,
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
                
                if (Physics.Raycast(ray, out Hit, 1000f, 512))
                {
                    if (Hit.transform.gameObject.tag == "Enemy")
                    {
                        // _selectedEnemy = Hit.transform.gameObject;
                        // _selectedPosition = Vector3.zero;
                        // _priorityEnemy = _selectedEnemy;
                    }
                }
                else if (Physics.Raycast(ray, out Hit, 1000f, layer))
                {
                    GetComponent<NavMeshAgent>().SetDestination(Hit.point);
                    // _selectedPosition = Hit.point;
                    // _selectedEnemy = null;
                }
            }
        }
    }

    private void AttackTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, AttackRange);
        bool goalExist = false;

        foreach (var el in hitColliders)
        {
            if (el.gameObject == _priorityEnemy.gameObject)
            {
                goalExist = true;
                if (!Attack)
                {
                    Attack = true;
                    animator.SetBool("isWalking", false);
                    animator.SetBool("shoot", true);
                    agent.SetDestination(transform.position);
                }
                
                if (_coroutine == null)
                {
                    _coroutine = StartCoroutine(StartAttack(el));
                }
            }
        }
        if (!goalExist && _coroutine != null)
        {
            Attack = false;
            animator.SetBool("shoot", false);
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    public void FindPriorityEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, MaxView);
        float minCost = Mathf.Infinity;

        foreach (var el in hitColliders)
        {
            if ((gameObject.CompareTag("Human") && el.gameObject.CompareTag("Enemy")) ||
                (gameObject.CompareTag("Enemy") && el.gameObject.CompareTag("Human")))
            {
                float cost = FindCost(el);
                if (cost < minCost)
                {
                    minCost = cost;
                    _priorityEnemy = el.gameObject;
                }
            }
        }
    }

    private float FindCost(Collider enemy)
    {
        Vector3 diff = enemy.transform.position - transform.position;
        float cost = diff.sqrMagnitude * diff.sqrMagnitude * enemy.GetComponent<Human>().HP;

        return cost;
    }

    IEnumerator StartAttack(Collider enemy)
    {
        if (CanShoot)
        {
            GameObject obj = Instantiate(bullet, transform.GetChild(1).position, Quaternion.FromToRotation(transform.GetChild(1).position, enemy.transform.position));
            obj.GetComponent<BulletController>().position = enemy.transform.position + new Vector3 (0, 1, 0);
            if (transform.gameObject.tag == "Enemy") obj.GetComponent<BulletController>().isEnemy = true;
            else obj.GetComponent<BulletController>().isEnemy = false;
            audioSrc.Play();
        }
        else
        {
            enemy.GetComponent<Human>().HP -= meleeDamage;
        }
        yield return new WaitForSeconds(0.6f);
        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}
