using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

public class SkinApplier : MonoBehaviour
{
    [SerializeField] private SkinLibrary library;     
    [SerializeField] private Transform skeletonRoot;  
    [SerializeField] private Transform mount;         
    [SerializeField] private string defaultSkinId = ""; 

    private readonly List<GameObject> currentMeshes = new();

    void Awake()
    {
        if (mount == null) mount = skeletonRoot != null ? skeletonRoot : transform;
    }

    void Start()
    {
        var id = string.IsNullOrEmpty(YG2.saves.selectedSkinId) ? defaultSkinId : YG2.saves.selectedSkinId;
        if (!string.IsNullOrEmpty(id))
            Apply(id);
    }

    public void Apply(string skinId)
    {
        var def = library.Find(skinId);
        if (def == null || def.skinPrefab == null)
        {
            Debug.LogWarning($"Skin '{skinId}' not found or prefab missing.");
            return;
        }
        
        foreach (var go in currentMeshes) if (go) Destroy(go);
        currentMeshes.Clear();
        
        var inst = Instantiate(def.skinPrefab);
        inst.name = def.skinPrefab.name + "(Applied)";
        
        var targetBones = skeletonRoot.GetComponentsInChildren<Transform>(true);
        var map = targetBones.ToDictionary(t => t.name, t => t);
        
        var smrs = inst.GetComponentsInChildren<SkinnedMeshRenderer>(true);
        foreach (var src in smrs)
        {
            var holder = new GameObject(src.gameObject.name);
            holder.transform.SetParent(mount, false);

            var dst = holder.AddComponent<SkinnedMeshRenderer>();
            dst.sharedMesh = src.sharedMesh;
            dst.sharedMaterials = src.sharedMaterials;
            
            var newBones = new Transform[src.bones.Length];
            for (int i = 0; i < newBones.Length; i++)
            {
                var boneName = src.bones[i] != null ? src.bones[i].name : null;
                if (!string.IsNullOrEmpty(boneName) && map.TryGetValue(boneName, out var t))
                    newBones[i] = t;
                else
                    Debug.LogWarning($"Bone '{boneName}' not found on target skeleton.");
            }
            dst.bones = newBones;
            
            if (src.rootBone != null && map.TryGetValue(src.rootBone.name, out var root))
                dst.rootBone = root;
            else
                dst.rootBone = skeletonRoot;

            currentMeshes.Add(holder);
        }
        
        Destroy(inst);

        Debug.Log($"Skin applied: {def.displayName}");
    }
}
