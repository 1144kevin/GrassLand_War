using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    public float jumpForce = 30f;
    public float gravityMoifer = 5f;
    public bool isOnGround = true;
    public bool gameover = false;
    private Animator animator;
    private float H_input;
    public float horizontalSpeed;
    public float Xrange = 15;
    public GameObject bulletPrefab;
    public Transform bulletTransform;
    public float bulletLifeTime = 1f; // 子彈的生命週期
    public float fastFallForce = 50f; // 快速下落的力量
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        if (Physics.gravity.y > -9.81f * gravityMoifer) // 確保重力只應用一次
        {
            Physics.gravity *= gravityMoifer;
        }
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        H_input = Input.GetAxis("Horizontal");
        // V_input = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * H_input * Time.deltaTime * horizontalSpeed);
        // transform.Translate(Vector3.forward * V_input * Time.deltaTime * speed);
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, bulletTransform.position, bulletPrefab.transform.rotation);

            // 銷毀實例化的子彈，延遲 bulletLifeTime 秒
            Destroy(bulletInstance, bulletLifeTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            animator.SetTrigger("Jump_trig");
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
        if (!isOnGround && Input.GetKey(KeyCode.S))
        {
            FastFall();
        }
        if (gameover == true)
        {
            animator.SetBool("Death_b", true);
            // animator.SetInteger("Death_Type_int", 1);
        }
    }
    void FastFall()
    {
        playerRB.AddForce(Vector3.down * fastFallForce, ForceMode.Acceleration);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameover = true;
            Debug.Log("gameover");
        }

    }
}
