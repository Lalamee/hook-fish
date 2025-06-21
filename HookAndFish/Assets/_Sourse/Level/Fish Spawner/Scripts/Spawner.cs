using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _fishPrefabs;
    [SerializeField] private float _secondsBetweenSpawn;

    private Transform[] _spawnPoints;
    private float _elapsedTime = 0f;

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
            CreateFishAtRandomPoint();
        }
    }

    private void CreateFishAtRandomPoint()
    {
        int spawnPointIndex = Random.Range(1, _spawnPoints.Length);
        Transform spawnPoint = _spawnPoints[spawnPointIndex];

        GameObject fishPrefab = GetRandomPrefab(_fishPrefabs);
        GameObject fish = Instantiate(fishPrefab, spawnPoint.position, spawnPoint.rotation);

        fish.transform.parent = spawnPoint;
    }

    private GameObject GetRandomPrefab(GameObject[] prefabs)
    {
        int randomIndex = Random.Range(0, prefabs.Length);
        return prefabs[randomIndex];
    }
}