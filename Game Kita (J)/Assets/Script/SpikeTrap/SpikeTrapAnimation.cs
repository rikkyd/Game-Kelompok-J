using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapAnimation : MonoBehaviour
{
    [Header("Trap Settings")]
    public int damage = 1; // Damage yang diberikan trap
    public float knockbackForce = 30f; // Kekuatan knockback

    private Collider2D playerCollider; // Menyimpan pemain yang ada di dalam trigger

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Deteksi apakah yang masuk ke trigger adalah Player
        if (collider.CompareTag("Player"))
        {
            playerCollider = collider; // Simpan referensi ke pemain
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        // Hapus referensi ketika pemain keluar dari trigger
        if (collider.CompareTag("Player"))
        {
            playerCollider = null;
        }
    }

    // Dipanggil oleh Animation Event
    public void ActivateTrap()
    {
        if (playerCollider != null)
        {
            // Ambil komponen PlayerController untuk memberikan damage
            PlayerController player = playerCollider.GetComponent<PlayerController>();
            if (player != null)
            {
                Debug.Log("Trap diaktifkan oleh Animation Event. Memberikan damage.");
                player.TakeDamage(damage);
            }

            // Tambahkan efek knockback (opsional)
            Rigidbody2D rb = playerCollider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 knockbackDirection = (playerCollider.transform.position - transform.position).normalized;
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}

