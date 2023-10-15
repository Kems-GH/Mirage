using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject resourcesPrefab;
    public GameObject MonsterPrefab;

    private float boundHorizontal = 24f;
    private float boundVertical = 9f;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        Spawn();
    }

    public void Spawn()
    {
        SpawnResources();
        SpawnMonster();
    }

    private Vector3 GenerateSpawnPosition(GameObject prefab)
    {
        float spawnPosX = Random.Range(-boundHorizontal, boundHorizontal);
        float spawnPosZ = Random.Range(-boundVertical, boundVertical);
        return new Vector3(spawnPosX, prefab.transform.position.y, spawnPosZ);
    }

    private void SpawnResources()
    {
        Instantiate(resourcesPrefab, GenerateSpawnPosition(resourcesPrefab), resourcesPrefab.transform.rotation);
    }

    private void SpawnMonster()
    {
        // Spawn a monster at a random position but not too close to the player
        Vector3 spawnPosition = GenerateSpawnPosition(MonsterPrefab);
        while (Vector3.Distance(spawnPosition, player.transform.position) < 5)
        {
            spawnPosition = GenerateSpawnPosition(MonsterPrefab);
        }
        Instantiate(MonsterPrefab, spawnPosition, MonsterPrefab.transform.rotation);
        
    }
}
