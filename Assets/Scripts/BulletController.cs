using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class BulletController : MonoBehaviour
{
    [NonSerialized]
    public Vector3 position;
    [NonSerialized]
    public bool isEnemy;
    public float speed = 30f;
    public int damage = 10;

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, step);
        transform.rotation = Quaternion.LookRotation(position - transform.position, Vector3.up);

        if (transform.position == position)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Human") && isEnemy) || (other.CompareTag("Enemy") && !isEnemy))
        {
            Human human = other.GetComponent<Human>();
            human.HP -= damage;
        }

        if ((other.CompareTag("Human") && !isEnemy) || (other.CompareTag("Enemy") && isEnemy))
        {

        }
        else Destroy(gameObject);
    }
}
