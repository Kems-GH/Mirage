using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject resourcesPrefab;
    public GameObject MonsterPrefab;

    private float boundHorizontal = 17f;
    private float boundVertical = 9f;
    private GameObject player;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        SpawnResources();
        yield return new WaitForSeconds(0.5f);
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
        if(gameManager.gameOver) return;
        Instantiate(resourcesPrefab, GenerateSpawnPosition(resourcesPrefab), resourcesPrefab.transform.rotation);
    }

    private void SpawnMonster()
    {
        if (gameManager.gameOver) return;
        // Spawn a monster at a random position but not too close to the player
        Vector3 spawnPosition = GenerateSpawnPosition(MonsterPrefab);
        while (Vector3.Distance(spawnPosition, player.transform.position) < 5)
        {
            spawnPosition = GenerateSpawnPosition(MonsterPrefab);
        }
        Instantiate(MonsterPrefab, spawnPosition, MonsterPrefab.transform.rotation);
        
    }
}
