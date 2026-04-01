using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 如果使用 TextMeshPro

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;  // 用來顯示時間的 Text
    private float elapsedTime = 0f;   // 遊戲經過的時間
    public static float initialSpeed = 25f; // 初始速度
    public float speedIncreaseInterval = 10f; // 每隔多少秒提升速度
    public float speedIncrement = 20f;        // 每次提升的速度值
    public float obstacledecrement = 0.2f;        // 每次提升的速度值
    private float nextSpeedIncreaseTime = 10f; // 下一次加速的時間點
    public PlayerController playerController_script;

    void Start()
    {
        ResetTimer(); // 初始化變數
        playerController_script = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {

        if (playerController_script.gameover == false)
        {// 計算經過的時間
            elapsedTime += Time.deltaTime;

            // 顯示時間（格式為分:秒）
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            if (timerText != null)
            {
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }

            if (elapsedTime >= nextSpeedIncreaseTime)
            {
                Debug.Log($"加速觸發！Elapsed Time: {elapsedTime}, Next Speed Increase Time: {nextSpeedIncreaseTime}");
                // 增加靜態速度
                MoveLeft.speed += speedIncrement;
                if (ObstacleSpawn.repeatRate > obstacledecrement)
                {
                    ObstacleSpawn.repeatRate -= obstacledecrement;
                }
                else
                {
                    ObstacleSpawn.repeatRate = 0.001f;
                }
                Debug.Log($"{MoveLeft.speed}");
                Debug.Log($"{ObstacleSpawn.repeatRate}");
                // 更新下一次加速的時間
                nextSpeedIncreaseTime += speedIncreaseInterval;
            }
        }
    }
    public void ResetTimer()
    {
        elapsedTime = 0f;
        MoveLeft.speed = initialSpeed;
        nextSpeedIncreaseTime = speedIncreaseInterval;
    }
}
