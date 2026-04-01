using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public static float speed = 25f;
    private static float initialSpeed = 25f; // 初始速度
    public PlayerController playerController_script;
    // Start is called before the first frame update
    void Start()
    {
        playerController_script = GameObject.Find("Player").GetComponent<PlayerController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (playerController_script.gameover == false)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        if (transform.position.y < -5 && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        };
    }
    public static void ResetSpeed()
    {
        speed = initialSpeed;
    }
}
