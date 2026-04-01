using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float startSpawnPos = 16;
    public float endSpawnPos = 16;
    public float spawnStartPos = -80;
    public float startDelay = 1.5f;
    public static float repeatRate = 2;
    public static float initialRepeatRate = 2;
    private PlayerController playerController_Script;

    void Start()
    {
        InvokeRepeating("obstacleSpawn", startDelay, repeatRate);
        playerController_Script = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        // if (currentObstacle != null && currentObstacle.transform.position.y < -5)
        // {
        //     Destroy(currentObstacle);
        //     SpawnObstacle();
        // }
    }
    void obstacleSpawn()
    {
        if (playerController_Script.gameover == false)
        {
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            float obstaclePositionX = Random.Range(startSpawnPos, endSpawnPos);
            Vector3 spawnPosition = new Vector3(obstaclePositionX, 150, spawnStartPos);
            Instantiate(obstaclePrefabs[obstacleIndex], spawnPosition, obstaclePrefabs[obstacleIndex].transform.rotation);
        }
    }
     public static void ResetRepeatRate()
    {
        repeatRate = initialRepeatRate;
    }
}