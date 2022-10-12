using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Bullet : MonoBehaviour
{
    [SerializeField] private Effects _effects;
    public Rigidbody Rigidbody;
    public float Speed = 5f;
    public int Damage = 1;
    public bool DestroyOnTouch = true;
    public Transform TrailTransform;
    private bool wasContact;
    private UnityEngine.GameObject OnDeathEffect;
    private void Start()
    {
        OnDeathEffect = _effects.EffectPrefabs[Random.Range(0, _effects.EffectPrefabs.Length)];
        Destroy(gameObject, 7f);
    }
    public void Shoot()
    {
        Rigidbody.AddForce(transform.right * Speed, ForceMode.Impulse);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (DestroyOnTouch)
        {
            if (!wasContact)
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy)
                {
                    Die();
                    wasContact = true;
                    enemy.TakeDamage(Damage);
                }
            }
        }
        else
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(Damage);
            }
        }
    }
    public void Die()
    {
        Instantiate(OnDeathEffect, transform.position, Quaternion.identity);
        TrailTransform.SetParent(null);
        Destroy(TrailTransform.gameObject, 1f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (DestroyOnTouch)
        {
            if (!wasContact)
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (enemy)
                {
                    wasContact = true;
                    enemy.TakeDamage(Damage);
                }
            }
            Die();
        }
        else
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(Damage);
            }
        }

    }
}
