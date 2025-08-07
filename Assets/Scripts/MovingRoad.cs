using System.Collections.Generic;
using UnityEngine;

public class InfiniteMovingRoad : MonoBehaviour
{
    [Header("Road Settings")] public GameObject roadSegmentPrefab;
    public int segmentsToKeep = 5;
    public float segmentLength = 20f;
    public float roadSpeed = 25f;

    private Queue<GameObject> activeSegments = new Queue<GameObject>();
    private float nextSpawnZ = 0f;
    private float destroyZPosition;

    void Start()
    {
        
        destroyZPosition = -segmentLength / 1.5f;

        // Инициализация начальных сегментов
        for (int i = 0; i < segmentsToKeep; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        
        MoveSegments();

        // проверка нужды в создание сегмента
        if (nextSpawnZ - roadSpeed * Time.deltaTime < segmentLength * segmentsToKeep)
        {
            SpawnSegment();
        }

        
        DeletePassedSegments();
    }

    void MoveSegments()
    {
        foreach (var segment in activeSegments)
        {
            segment.transform.Translate(0, 0, -roadSpeed * Time.deltaTime);
        }

        nextSpawnZ -= roadSpeed * Time.deltaTime;
    }

    void SpawnSegment()
    {
        GameObject newSegment = Instantiate(
            roadSegmentPrefab,
            new Vector3(0, 0, nextSpawnZ),
            Quaternion.identity,
            transform 
        );

        activeSegments.Enqueue(newSegment);
        nextSpawnZ += segmentLength;
    }

    void DeletePassedSegments()
    {
        while (activeSegments.Count > 0 &&
               activeSegments.Peek().transform.position.z < destroyZPosition)
        {
            GameObject oldSegment = activeSegments.Dequeue();
            Destroy(oldSegment);
        }
    }
}    