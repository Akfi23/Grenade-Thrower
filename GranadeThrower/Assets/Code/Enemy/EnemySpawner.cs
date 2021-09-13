using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : ObjectPool
{
    [SerializeField] private float _secondsPerSpawn;
    [SerializeField] private float _currentTimer = 0;

    private void Start()
    {
        InitializeEnemies();
    }

    private void Update()
    {
        _currentTimer += Time.deltaTime;
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        if (_currentTimer >= _secondsPerSpawn)
        {
            if (TryGetEnemy(out GameObject enemy))
            {
                _currentTimer = 0;

                Respawn(enemy);
            }
        }
    }

    private void Respawn(GameObject enemy)
    {
        enemy.SetActive(true);

        enemy.transform.position = enemy.GetComponent<Enemy>().StartPosition;
    }
}
