using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilleHarimau : MonoBehaviour
{
    public float speed;

    [Header("Explosion Settings")]
    public GameObject smallProjectilePrefab; 
    public int numberOfSmallProjectiles = 1; // jumlah prefab projektil keicl yang keluar nanti
    public float smallProjectileSpeed = 0f;

    private Transform player;
    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        for (int i = 0; i < numberOfSmallProjectiles; i++)
        {
            // Hitung sudut di mana proyektil kecil akan ditembakkan
            //niatnya pengen ke summon lebih banyak, tapi gw gak terlalu paham sama rumusnya, jadinya gw projektil ini bakal ninggalin projektil kecil aja
            float angle = i * (360f / numberOfSmallProjectiles);
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            // Buat proyektil kecil
            GameObject smallProjectile = Instantiate(smallProjectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = smallProjectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * smallProjectileSpeed; // Tetapkan kecepatan proyektil kecil
            }
        }
        Destroy(gameObject);
    }

  
}
