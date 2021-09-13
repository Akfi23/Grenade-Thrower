using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : ObjectPool
{
    [SerializeField] private float _secondsPerSpawn;

    private void Start()
    {
        InitializeEnemies();
    }

    private void Update()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {       
        if (TryGetEnemy(out GameObject enemy))
        {
           StartCoroutine(Respawn(enemy));
        }
    }
   
    IEnumerator Respawn(GameObject enemy) 
    {
        yield return new WaitForSeconds(_secondsPerSpawn);
        enemy.transform.position = enemy.GetComponent<Enemy>().StartPosition;
        enemy.SetActive(true);
    }
}
