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

    private float _maxHealthBarScale;

    private AudioSource audioSrc;

    public void Awake() 
    {
        HP = MaxHP;
        MainCamera = Camera.main;
        Target = gameObject.transform.position;
        audioSrc = GetComponent<AudioSource>();

        if (transform.tag == "Human") _maxHealthBarScale = transform.GetChild(0).transform.localScale.x;
    }

    public void Update()
    {
        if (HP <= 0) Destroy(gameObject);
        if (transform.tag == "Human") UpdateHealthBar();
        SetTarget();

        FindPriorityEnemy();
        
        if (!Attack && _priorityEnemy != null)
        {
            GetComponent<NavMeshAgent>().SetDestination(_priorityEnemy.transform.position);
        }
        AttackTarget();
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
                    GetComponent<NavMeshAgent>().SetDestination(transform.position);
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
