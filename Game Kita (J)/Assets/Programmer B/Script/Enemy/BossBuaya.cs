using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBuaya : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public float stopDistance;
    public float retreatDistance;
    public float detectionRange;
    public float startTimeBetweenShot;
    private float timeBetweenShot;
    public int health = 4;

    [Header("Reference")]
    public GameObject projectile;
    private Transform player;
    private LayerMask obstacleLayer;
    private bool playerDetected;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("player gak ada.");
            return;
        }

        timeBetweenShot = startTimeBetweenShot;
        obstacleLayer = LayerMask.GetMask("Object");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead || player == null) return; // di return kalo player gak ditemukan atau mati

        HandlePlayerDetection();
        if (playerDetected)
        {
            if (!IsPathBlocked(player.position))
            {
                HandleChaseOrRetreat();
                Shooting();
            }
        }
    }

    private void HandlePlayerDetection()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        playerDetected = distanceToPlayer <= detectionRange;
    }

    private void HandleChaseOrRetreat()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > stopDistance) //kalo player jauh, musuh mendekat
        {
            MoveTowards(player.position, speed);
        }
        else if (distanceToPlayer < retreatDistance) // kalo player terlalu dekat, musuh bakal menjauh
        {
            MoveTowards(player.position, -speed);
        }
    }

    private bool IsPathBlocked(Vector2 targetPosition)
    {
        Vector2 direction = targetPosition - (Vector2)transform.position;
        float distance = direction.magnitude;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, obstacleLayer);

        return hit.collider != null;
    }

    private void MoveTowards(Vector2 targetPosition, float moveSpeed)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void Shooting()
    {
        if (timeBetweenShot <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBetweenShot = startTimeBetweenShot;
        }
        else
        {
            timeBetweenShot -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        isDead = true;
        // Tambahin animasi mati disini
        this.enabled = false;
    }

    private void OnDrawGizmos()
    {
        // penanda zona deteksi 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

