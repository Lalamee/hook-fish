using UnityEngine;

[ExecuteInEditMode]
public class IKControl : MonoBehaviour
{
    [Header("Targets")]
    [Tooltip("Объект, к которому будет тянуться кисть")]
    public Transform target;

    [Tooltip("Кисть (конечный эффектор)")]
    public Transform hand;

    [Header("Настройки IK")]
    [Range(1, 20)]
    [Tooltip("Сколько итераций CCD за кадр")]
    public int iterations = 10;

    [Tooltip("Минимальное расстояние до цели, после которого итерации прекращаются")]
    public float threshold = 0.01f;

    private Transform elbow;
    private Transform shoulder;

    void Start()
    {
        if (hand == null)
        {
            Debug.LogError("IKControl: не назначен Transform руки (hand)");
            enabled = false;
            return;
        }

        elbow = hand.parent;
        if (elbow == null)
        {
            Debug.LogError("IKControl: у руки нет родителя (локтя)");
            enabled = false;
            return;
        }

        shoulder = elbow.parent;
        if (shoulder == null)
        {
            Debug.LogError("IKControl: у локтя нет родителя (плеча)");
            enabled = false;
            return;
        }
    }

    void LateUpdate()
    {
        if (target == null || hand == null) return;
        
        for (int i = 0; i < iterations; i++)
        {
            SolveJoint(elbow);
            SolveJoint(shoulder);

            if ((hand.position - target.position).sqrMagnitude < threshold * threshold)
                break;
        }
    }
    
    private void SolveJoint(Transform joint)
    {
        Vector3 toEffector = (hand.position - joint.position);
        Vector3 toTarget   = (target.position - joint.position);

        if (toEffector.sqrMagnitude < 0.0001f || toTarget.sqrMagnitude < 0.0001f)
            return;

        // вычисляем вращение, переводя вектор toEffector → toTarget
        Quaternion rot = Quaternion.FromToRotation(toEffector, toTarget);
        joint.rotation = rot * joint.rotation;
    }
}
