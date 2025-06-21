using UnityEngine;

public class AreaForBoat : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private FishingStoper _fishingStoper;
    [SerializeField] private CountTrappedFish _progress;
    [SerializeField] private Collider _leftDestroyZone;
    [SerializeField] private Collider _rightDestroyZone;
    [SerializeField] private PlayerLevel _level;

    public void StartSpawnFish()
    {
        _spawner.enabled = true;
        _fishingStoper.enabled = true;
        _progress.enabled = true;
        _leftDestroyZone.enabled = true;
        _rightDestroyZone.enabled = true;
        _level.gameObject.SetActive(true);
    }
}
