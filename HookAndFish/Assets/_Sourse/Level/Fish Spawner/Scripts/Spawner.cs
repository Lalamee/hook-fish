using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _fishPrefabs;
    [SerializeField] private float _secondsBetweenSpawn;

    private Transform[] _spawnPoints;
    private float _elapsedTime = 0f;

    private Player _player;
    private int? _previousFishLevel;

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<Transform>();
        _player = FindObjectOfType<Player>();
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
        GameObject fishObject = Instantiate(fishPrefab, spawnPoint.position, spawnPoint.rotation);
        fishObject.transform.parent = spawnPoint;

        Fish fish = fishObject.GetComponent<Fish>();

        if (fish != null)
        {
            int level = GenerateFishLevel();
            fish.SetLevel(level);
            _previousFishLevel = level;
        }
    }

    private GameObject GetRandomPrefab(GameObject[] prefabs)
    {
        int randomIndex = Random.Range(0, prefabs.Length);
        return prefabs[randomIndex];
    }

    private int GenerateFishLevel()
    {
        int playerLevel = _player.GetLevel();
        int firstLevel = Mathf.Min(1, _player.GetStartLevel());
        int levelStep = (int)Random.Range(firstLevel, playerLevel + 10);

        if (_previousFishLevel.HasValue && _previousFishLevel.Value < playerLevel)
        {
            return Mathf.Min(playerLevel + 1, playerLevel + levelStep);
        }

        float valueForSet = Random.Range(0f, 10f);
        float halfValue = 9f;

        if (valueForSet <= halfValue)
        {
            return Random.Range(firstLevel, playerLevel + levelStep);
        }
        else
        {
            return playerLevel;
        }
    }
}
