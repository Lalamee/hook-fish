using UnityEngine;
using YG;

public class PlayerSkinApplier : MonoBehaviour
{
    [Header("Доступные скины (те же SO, что и в магазине)")]
    [SerializeField] private SkinDefinition[] skins;

    [Header("Куда инстансить префаб скина")]
    [SerializeField] private Transform skinParent;

    private GameObject _current;

    private void Start()
    {
        ApplySelectedSkin();
    }

    public void ApplySelectedSkin()
    {
        // Если ничего не выбрано — ничего не подставляем.
        if (string.IsNullOrEmpty(YG2.saves.selectedSkinId))
        {
            Debug.Log("SelectedSkinId пуст — скин не подставляем.");
            return;
        }

        string id = YG2.saves.selectedSkinId;
        var def = System.Array.Find(skins, s => s.id == id);
        if (def == null || def.skinPrefab == null)
        {
            Debug.LogWarning($"Скин '{id}' не найден в массиве или у него не задан prefab.");
            return;
        }

        if (_current != null) Destroy(_current);

        _current = Instantiate(def.skinPrefab, skinParent);
        _current.transform.localPosition = Vector3.zero;
        _current.transform.localRotation = Quaternion.identity;
        _current.transform.localScale = Vector3.one;
    }
}