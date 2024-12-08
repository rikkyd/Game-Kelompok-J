using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScripts : MonoBehaviour
{
    public float moveSpeed = 1f; // Kecepatan panah
    public float timetoLife = 4f; // Waktu hidup panah
    private float timesinceSpawned = 0f; // Waktu sejak panah dibuat
    public float damage = 1f; // Besar kerusakan panah
    public float knockbackForce = 30f; // Kekuatan knockback panah

    // Update is called once per frame
    void Update()
    {
        // Menggerakkan panah
        transform.position += moveSpeed * transform.right * Time.deltaTime;

        // Menghitung waktu hidup panah
        timesinceSpawned += Time.deltaTime;
        if (timesinceSpawned >= timetoLife)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Periksa apakah objek yang ditabrak adalah pemain
        if (collider.CompareTag("Player"))
        {
            // Menerapkan knockback jika pemain memiliki Rigidbody2D
            Rigidbody2D playerRigidbody = collider.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                // Hitung arah knockback
                Vector2 knockbackDirection = new Vector2(collider.transform.position.x - transform.position.x, 0).normalized;
                playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }

            // Hancurkan panah setelah mengenai pemain
            Destroy(gameObject);
        }
    }
}
