using UnityEngine;

public class PlayerEntranceColliderSpawner : MonoBehaviour
{
    [SerializeField] private GameObject entranceColliderPrefab = null;
    [SerializeField] private EntrancePointsHolder entrancePointsHolder = null;
    [SerializeField] private Transform parent = null;
    [SerializeField] private float offset = 0.3f;
    private void Awake()
    {
        entrancePointsHolder.OnValueChanged += OnValueChanged;
    }

    private void OnDestroy()
    {
        entrancePointsHolder.OnValueChanged -= OnValueChanged;
    }

    private void OnValueChanged()
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
        foreach (EntrancePoint entrancePoint in entrancePointsHolder.ListOfEntrancePoints)
        {
            GameObject point = Instantiate<GameObject>(entranceColliderPrefab, parent);
            point.transform.position = point.transform.TransformPoint(entrancePoint.entrancePoint - (entrancePoint.entrancePoint -entrancePoint.exitPoint).normalized * offset);
            point.transform.rotation = point.transform.rotation * Quaternion.Euler(0, 0, entrancePoint.angleFromRight);
        }
    }
}
