using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public float startSpawnPos = 16;
    public float endSpawnPos = 16;
    public float spawnStartPos = -80;
    public float spawnInterval = 2.0f;
    public PlayerController playerController_script;
    void Start()
    {
        playerController_script = GameObject.Find("Player").GetComponent<PlayerController>();
        // 開始每隔一段時間生成動物
        InvokeRepeating("SpawnAnimal", 0, spawnInterval);
    }

    // 生成動物的函數
    void SpawnAnimal()
    {
        if (playerController_script.gameover == false)
        {
            int animalIndex = Random.Range(0, animalPrefabs.Length);
            float animalPositionZ = Random.Range(startSpawnPos, endSpawnPos);
            Vector3 spawnPosition = new Vector3(spawnStartPos, 150, animalPositionZ);
            Instantiate(animalPrefabs[animalIndex], spawnPosition, animalPrefabs[animalIndex].transform.rotation);
        }
    }
}
