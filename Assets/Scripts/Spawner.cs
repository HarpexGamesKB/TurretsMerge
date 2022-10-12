using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private bool _canSpawn;
    [SerializeField] private Enemy EnemyPrefab;
    [SerializeField] private float _waitTime = 4;
    private void Start()
    {
        if (_canSpawn)
        {
            InfinitySpawn();
        }
    }
    private void InfinitySpawn()
    {
        StartCoroutine(WaitBeforeSpawn());
    }
    public void SpawnOneEnemy()
    {
        Enemy enemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
        enemy.Rigidbody.AddTorque(new Vector3(Random.value, Random.value, Random.value));
        //for infinity
        InfinitySpawn();
    }
    private IEnumerator WaitBeforeSpawn()
    {
        yield return new WaitForSeconds(_waitTime);
        SpawnOneEnemy();
    }
}
