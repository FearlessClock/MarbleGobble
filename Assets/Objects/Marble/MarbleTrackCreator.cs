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
        for (int i = trackParent.childCount; i < spawnPointsHolder.ListOfSpawnPoints.Count; i++)
        {
            SpawnPoint point = spawnPointsHolder.ListOfSpawnPoints[i];
            TrackController trackController = Instantiate<TrackController>(trackPrefab, trackParent);
            trackController.startingPoint = point.middlePoint;
            trackController.angle = point.angleFromRight;
            trackController.gameObject.SetActive(true);
        }
        trackPrefab.gameObject.SetActive(true);
    }
}
