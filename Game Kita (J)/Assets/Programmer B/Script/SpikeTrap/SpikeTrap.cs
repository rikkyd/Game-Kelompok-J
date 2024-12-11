using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{

    public float knockbackForce = 30f; // Kekuatan knockback panah
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }
}
