using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameTimer gameTimer;
    public GameObject startMenuCanvas;   // 開始介面的 Canvas
    public GameObject gameOverCanvas;   // 遊戲結束的 Canvas
    public PlayerController playerController; // 玩家控制腳本

    private static bool isRestarting = false; // 標記是否是重啟

    void Start()
    {
        if (isRestarting)
        {
            // 如果是重啟，跳過 StartMenuCanvas
            startMenuCanvas.SetActive(false);
            gameOverCanvas.SetActive(false);
            Time.timeScale = 1f; // 確保遊戲繼續進行
            isRestarting = false; // 重置標記
        }
        else
        {
            // 初次進入遊戲，顯示 StartMenuCanvas
            startMenuCanvas.SetActive(true);
            gameOverCanvas.SetActive(false);
            Time.timeScale = 0f; // 暫停遊戲
        }
    }

    void Update()
    {
        if (playerController != null && playerController.gameover)
        {
            // 顯示 Game Over 畫面
            ShowGameOver();
        }
    }

    public void StartGame()
    {
        startMenuCanvas.SetActive(false);
        Time.timeScale = 1f; // 恢復遊戲
    }

    void ShowGameOver()
    {
        gameOverCanvas.SetActive(true);
    }

     public void RestartGame()
    {
        // 重置遊戲邏輯
        ScoreManager.score = 0;
        gameTimer.ResetTimer(); // 重置計時器和速度
        MoveLeft.ResetSpeed();  // 確保速度重置（可選，視情況）
        ObstacleSpawn.ResetRepeatRate();

        // 重啟場景
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        // 設置重啟標記
        isRestarting = true;
    }
}
