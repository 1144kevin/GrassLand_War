using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 如果使用 TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // 在 Inspector 中連結 UI 元素
    public static int score;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        scoreText.text = "Score: "+Mathf.Round(score);
    }
}