using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{
    [SerializeField] private float _delay = 2.5f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private Enemy CurrentTargetEnemy;
    [SerializeField] private List<Enemy> Enemies;
    [SerializeField] private bool _startShoot = true;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform[] Spawnpoints;

    
    void Update()
    {
        if (CurrentTargetEnemy != null)
        {
            Vector2 direction = CurrentTargetEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * _rotationSpeed);
        }
        else
        {
            CurrentTargetEnemy = TurretsController.Instance.FindClosestEnemy(transform);
        }
    }

    void FixedUpdate()
    {
        if (_startShoot)
        {
            StartShooting();
            _startShoot = false;
        }
    }
    private void StartShooting()
    {
        StartCoroutine(WaitBeforeShoot());
    }
    private void Shoot()
    {
        if (CurrentTargetEnemy == null) return;
        for (int i = 0; i < Spawnpoints.Length; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab, Spawnpoints[i].position, transform.rotation);
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            bulletComponent.Shoot();
        }
    }
    private IEnumerator WaitBeforeShoot()
    {
        yield return new WaitForSeconds(_delay);
        Shoot();
        StartShooting();
    }
}
