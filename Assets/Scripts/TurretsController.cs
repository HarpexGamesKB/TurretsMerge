using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretsController : Singleton<TurretsController>
{
    [SerializeField] public List<Enemy> AllEnemies;
    //[SerializeField] private List<Turret> Turrets;
    [SerializeField] private int maxTurretsOnOneTarget = 2;
    [SerializeField] private int currentTurretsOnOneTargetCount=0;
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            AllEnemies.Add(enemy);
        }
    }
    public Enemy FindClosestEnemy(Transform turretTransform)
    {
        float distance = Mathf.Infinity;
        Vector3 position = turretTransform.position;
        Enemy enemy = FindObjectOfType<Enemy>();
        for (int i = 0; i < AllEnemies.Count; i++)
        {
            if (AllEnemies[i] == null)
            {
                //Enemies.Remove(Enemies[i]);
                continue;
            }
            Vector3 diff = AllEnemies[i].transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                //
                distance = curDistance;
                enemy = AllEnemies[i];
            }
        }
        //for different targets
        currentTurretsOnOneTargetCount++;
        if(currentTurretsOnOneTargetCount >= maxTurretsOnOneTarget)
        {
            currentTurretsOnOneTargetCount = 0;
            AllEnemies.Remove(enemy);
        }
        return enemy;
    }
    /*
    public Enemy FindFirstEnemy(Transform turretTransform)
    {
        Enemy enemy = FindObjectOfType<Enemy>();

        enemy = AllEnemies[0];

        return enemy;
    }
    public Enemy FindLastEnemy(Transform turretTransform)
    {
        Enemy enemy = FindObjectOfType<Enemy>();

        enemy = AllEnemies[AllEnemies.Count];

        return enemy;
    }
    public Enemy FindRandomEnemy(Transform turretTransform)
    {
        
        Enemy enemy = FindObjectOfType<Enemy>();

        enemy = AllEnemies[Random.Range(0, AllEnemies.Count)];

        return enemy;
    }*/
}
