using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public GameObject[] terrainPrefabs; // 地形預製件
    public List<GameObject> initialTerrains; // 初始地形（設置在場景中）
    public float moveSpeed = 10f; // 地形移動速度
    public Transform player; // 玩家 Transform
    public float overlapDistance = 5f; // 地形重疊距離

    private Queue<GameObject> activeTerrains = new Queue<GameObject>(); // 存儲當前所有活動地形
    private float terrainLength; // 地形的長度

    void Start()
    {
        terrainLength = GetTerrainLength();

        // 初始化已有地形，加入 activeTerrains 隊列中
        foreach (GameObject terrain in initialTerrains)
        {
            activeTerrains.Enqueue(terrain);
        }

        // 額外生成一個地形
        SpawnTerrain(GetNextSpawnPosition());
    }

    void Update()
    {
        // 移動所有地形
        foreach (GameObject terrain in activeTerrains)
        {
            terrain.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        // 銷毀超過玩家的地形並生成新地形
        if (IsTerrainBehindPlayer(activeTerrains.Peek()))
        {
            GameObject oldTerrain = activeTerrains.Dequeue();
            Destroy(oldTerrain);

            // 生成新的地形，並加入到列表中
            SpawnTerrain(GetNextSpawnPosition());
        }


        // 保證至少有一個地形存在（防止場景中所有地形被銷毀）
        if (activeTerrains.Count == 0)
        {
            Debug.LogWarning("No active terrains! Generating a new one...");
            SpawnTerrain(player.position.z + terrainLength);
        }
    }

    private void SpawnTerrain(float zPosition)
    {
        // 隨機選擇地形預製件
        int randomIndex = Random.Range(0, terrainPrefabs.Length);
        GameObject terrain = Instantiate(terrainPrefabs[randomIndex], new Vector3(0, 0, zPosition), Quaternion.identity);

        // 將新生成的地形加入到 activeTerrains 中
        activeTerrains.Enqueue(terrain);

    }

    private float GetTerrainLength()
    {
        // 計算第一個地形的長度
        Bounds combinedBounds = new Bounds(terrainPrefabs[0].transform.position, Vector3.zero);
        Renderer[] renderers = terrainPrefabs[0].GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            combinedBounds.Encapsulate(renderer.bounds);
        }

        return combinedBounds.size.z;
    }

    private float GetNextSpawnPosition()
    {
        GameObject lastTerrain = activeTerrains.ToArray()[activeTerrains.Count - 1];
        return lastTerrain.transform.position.z + terrainLength - overlapDistance;
    }

    private bool IsTerrainBehindPlayer(GameObject terrain)
    {
        // 判斷地形是否已經完全超過玩家
        return terrain.transform.position.z + terrainLength < player.position.z;
    }
}
