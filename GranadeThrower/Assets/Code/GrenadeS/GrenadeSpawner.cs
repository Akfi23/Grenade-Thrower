using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnPoints =new List<GameObject>();
    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] private GrenadPicker grenadPicker;
    [SerializeField] private float RespawnTime;

    private void OnEnable()
    {
        grenadPicker.PickedUpGrenade +=SpawnNewGrenade;
    }

    private void OnDisable()
    {
        grenadPicker.PickedUpGrenade -= SpawnNewGrenade;
    }

    private void Start()
    {
        InitializeGrenades();
    }

    private void Update()
    {
       
    }

    private void InitializeGrenades()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        { 
            int rand = Random.Range(0, prefabs.Count);
            Instantiate(prefabs[rand], spawnPoints[i].transform.position, Quaternion.identity,gameObject.transform);
        }        
    }

    private void SpawnNewGrenade(Vector3 position) 
    {
        StartCoroutine(SpawnGrenadeAtSamePosition(position));
    }
    
    IEnumerator SpawnGrenadeAtSamePosition(Vector3 pos) 
    {
        yield return new WaitForSeconds(RespawnTime);
        int rand = Random.Range(0, prefabs.Count);
        Instantiate(prefabs[rand], pos, Quaternion.identity,gameObject.transform);
    }
}
