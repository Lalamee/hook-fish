using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerFishMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] _fishPrefabs;
    [SerializeField] private float _secondsBetweenSpawn;

    private Transform[] _spawnPoints;
    private float _elapsedTime = 0f;
    private int _previousSpawnIndex = -1;

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _secondsBetweenSpawn)
        {
            _elapsedTime = 0f;
            SpawnFish();
        }
    }

    private void SpawnFish()
    {
        int spawnPointIndex = GetRandomSpawnPointIndex();
        Transform spawnPoint = _spawnPoints[spawnPointIndex];

        GameObject fishPrefab = GetRandomPrefab(_fishPrefabs);
        GameObject fishObject = Instantiate(fishPrefab, spawnPoint.position, spawnPoint.rotation);
        fishObject.transform.parent = spawnPoint;
    }

    private GameObject GetRandomPrefab(GameObject[] prefabs)
    {
        int randomIndex = Random.Range(0, prefabs.Length);
        return prefabs[randomIndex];
    }

    private int GetRandomSpawnPointIndex()
    {
        int index;
        do
        {
            index = Random.Range(1, _spawnPoints.Length); 
        } while (index == _previousSpawnIndex);

        _previousSpawnIndex = index;
        return index;
    }
}
