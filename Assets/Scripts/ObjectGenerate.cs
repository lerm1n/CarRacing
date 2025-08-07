using System;
using UnityEditor;
using UnityEngine;

public class ObjectGenerate : MonoBehaviour
{
    public GameObject[] objectsPrefabs;
    public float spawnDistance = 20f;
    public float spawnInterval = 2f;
    public float laneDistance = 3f;
    public Transform player;

    private float nextSpawnTime;
    
    
    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (player == null) Debug.LogError("Player не найден!");
        }

        Debug.Log($"Префабов: {objectsPrefabs.Length}");
    }

    private void Update()
    {
        if (player == null || objectsPrefabs.Length == 0) return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnObjects();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnObjects()
    {
        int lane = UnityEngine.Random.Range(0, 3);
        Vector3 spawnPos = player.position + Vector3.forward * spawnDistance;
        spawnPos.x = (lane - 1) * laneDistance;

        GameObject obstacle = objectsPrefabs[UnityEngine.Random.Range(0, objectsPrefabs.Length)];
        Instantiate(obstacle, spawnPos, Quaternion.identity);
        
        Debug.Log($"Создан объект {obstacle.name} на позиции {spawnPos}");
    }
    
    
}
