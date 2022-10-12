using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public Rigidbody Rigidbody;
    [SerializeField] protected float _speed = 5f;
    [SerializeField] protected float _maxVelosity = 5f;
    [SerializeField] protected int _health = 1;
    [SerializeField] protected UnityEngine.GameObject OnDeathEffect;
    [SerializeField] protected bool isDead;

    public virtual void FixedUpdate()
    {/*
        if (Rigidbody.velocity.y > -_maxVelosity)
        {
            Rigidbody.AddForce(_speed * Time.deltaTime * Vector3.down, ForceMode.Impulse);
        }*/
    }
    public virtual void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            if (!isDead)
            {
                isDead = true;
                Die();
            }
        }
    }
    
    public virtual void Die()
    {
        Instantiate(OnDeathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
