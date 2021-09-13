using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();

    [SerializeField] private GameObject _enemy;
  
    protected void InitializeEnemies()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            GameObject newEnemy= Instantiate(_enemy, spawnPoints[i].transform.position, Quaternion.identity,gameObject.transform);
            _enemies.Add(newEnemy);
        }
    }

    protected bool TryGetEnemy(out GameObject result)
    {
        result = _enemies.First(enemy => enemy.activeSelf == false);
        return result != null;
    }
}
