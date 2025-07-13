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

    private int _previousSpawnIndex = -1;
    private int _leftStreak = 0;
    private int _rightStreak = 0;

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
        int spawnPointIndex = GetValidSpawnPointIndex();
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

        UpdateSpawnSideTracking(spawnPointIndex);
        _previousSpawnIndex = spawnPointIndex;
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
        int levelStep = Random.Range(firstLevel, playerLevel + 10);

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

    private int GetValidSpawnPointIndex()
    {
        int maxAttempts = 20;
        int half = _spawnPoints.Length / 2;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            int index = Random.Range(1, _spawnPoints.Length);

            if (index == _previousSpawnIndex)
                continue; 

            bool isLeft = index < half;
            bool isRight = index >= half;

            if (_leftStreak >= 2 && isLeft) continue;
            if (_rightStreak >= 2 && isRight) continue;

            return index;
        }
        
        return (_previousSpawnIndex + 1) % (_spawnPoints.Length - 1) + 1;
    }

    private void UpdateSpawnSideTracking(int index)
    {
        int half = _spawnPoints.Length / 2;
        bool isLeft = index < half;
        bool isRight = index >= half;

        if (isLeft)
        {
            _leftStreak++;
            _rightStreak = 0;
        }
        else if (isRight)
        {
            _rightStreak++;
            _leftStreak = 0;
        }
    }
}
