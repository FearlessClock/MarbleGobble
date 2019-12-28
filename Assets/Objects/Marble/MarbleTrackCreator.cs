using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleTrackCreator : MonoBehaviour
{
    [SerializeField] private SpawnPointsHolder spawnPointsHolder = null;
    [SerializeField] private TrackController trackPrefab = null;
    [SerializeField] private Transform trackParent = null;

    private void Awake()
    {
        spawnPointsHolder.OnValueChanged += (GenerateTracks);
    }

    private void OnDestroy()
    {
        spawnPointsHolder.OnValueChanged -= (GenerateTracks);
    }
    private void GenerateTracks()
    {
        trackPrefab.gameObject.SetActive(false);
        foreach (Transform child in trackParent)
        {
            Destroy(child.gameObject);
        }
        foreach(SpawnPoint point in spawnPointsHolder.ListOfSpawnPoints)
        {
            TrackController trackController = Instantiate<TrackController>(trackPrefab, trackParent);
            trackController.startingPoint = point.middlePoint;
            trackController.angle = point.angleFromRight;
            trackController.gameObject.SetActive(true);
        }
        trackPrefab.gameObject.SetActive(true);
    }
}
