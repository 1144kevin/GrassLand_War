using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryBullet : MonoBehaviour
{
    public float lifeTime = 3f; // 子彈的生命週期
    public ParticleSystem particleEffect; // 預設存放在物件內的特效
    public AudioClip hitSound; // 撞擊音效
    private AudioSource audioSource; // 用來播放音效的音源

    // Start is called before the first frame update
    void Start()
    {
        if (particleEffect == null)
        {
            particleEffect = GetComponentInChildren<ParticleSystem>();
        }
        if (hitSound == null)
        {
            Debug.LogWarning("Hit sound not assigned!");
        }

        audioSource = GetComponent<AudioSource>(); // 確保有 AudioSource 組件
        Destroy(gameObject, lifeTime); // 子彈生命週期結束後自動銷毀
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("animal"))
        {
            Debug.Log("Hit animal!");

            // 播放擊中音效
            if (audioSource != null && hitSound != null)
            {
                audioSource.PlayOneShot(hitSound); // 播放單次音效
            }

            if (particleEffect != null)
            {
                // 分離特效，避免因子彈被銷毀而影響特效
                particleEffect.transform.parent = null;
                particleEffect.Play();

                // 銷毀特效對象（延遲至播放完成）
                Destroy(particleEffect.gameObject, particleEffect.main.duration);
            }

            // 銷毀子彈
            Destroy(gameObject, hitSound.length);
        }
    }
}