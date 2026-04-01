using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutBounds : MonoBehaviour
{
    public float rightBound = 100;
    public float leftBound = -80;
    public float downBound = 50; // 玩家 Transform

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > rightBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.y < downBound)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            if (gameObject.CompareTag("animal"))
            {
                Destroy(gameObject);
                ScoreManager.score += 1;
            }
        }

        if (collision.gameObject.CompareTag("exitObstacle"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("forest"))
        {
            Destroy(gameObject);
        }
    }

}
