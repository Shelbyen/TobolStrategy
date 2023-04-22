using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class BulletController : MonoBehaviour
{
    [NonSerialized]
    public Vector3 position;
    public float speed = 30f;
    public int damage = 10;

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, step);
        transform.localRotation = Quaternion.FromToRotation(transform.position, position);

        if (transform.position == position)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Human"))
        {
            Human human = other.GetComponent<Human>();
            human.HP -= damage;
        }
        Destroy(gameObject);
    }
}
