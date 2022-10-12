using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : Enemy
{
    public Transform[] SpawnPoints;
    public Enemy EnemyToCreate;
    public float ExplosionForce = 10f;
    public float ExplosionRadius = 1f;
    public override void Die()
    {
        transform.localScale *= 1.1f;
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            Enemy enemy = Instantiate(EnemyToCreate,new Vector3(SpawnPoints[i].position.x, SpawnPoints[i].position.y, 0f), Quaternion.identity);
            enemy.Rigidbody.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius);
            //enemy.Rigidbody.AddTorque(new Vector3(Random.value, Random.value, Random.value));
        }
        base.Die();
        
    }
}
