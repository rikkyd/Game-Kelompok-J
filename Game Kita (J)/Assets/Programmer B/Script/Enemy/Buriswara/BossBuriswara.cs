using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBuriswara : MonoBehaviour
{
    [Header("Boss Stats")]
    public int maxHealth = 10;
    private int currentHealth;

    [Header("Phase 1 Settings")]
    public float moveSpeedPhase1 = 2f;
    public float attackCooldownPhase1 = 2f;

    [Header("Phase 2 Settings")]
    public float moveSpeedPhase2 = 4f;
    public float attackCooldownPhase2 = 1f;

    [Header("Detection Settings")]
    public float detectionRange = 5f;
    public LayerMask obstacleLayer; 

    [Header("Attack Settings")]
    public float attackRange = 1.5f;
    public int damage = 10;

    private Transform player;
    private float attackCooldown;
    private bool isPhase2;

    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
        attackCooldown = attackCooldownPhase1;
        isPhase2 = false;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogError("Player tidak ditemukan! Pastikan player memiliki tag 'Player'.");
        }
    }

    private void Update()
    {
        if (player == null) return;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && !IsPathBlocked(player.position))
        {
            if (distanceToPlayer > attackRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                AttackPlayer();
            }
        }
        else
        {
            // Jika pemain di luar jarak deteksi atau terhalang, berhenti bergerak
            animator.SetBool("IsMoving", false);
        }

        // Periksa apakah fase kedua aktif
        if (!isPhase2 && currentHealth <= maxHealth / 2)
        {
            ActivatePhase2();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float speed = isPhase2 ? moveSpeedPhase2 : moveSpeedPhase1;
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);

        // Perbarui animasi
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
        animator.SetBool("IsMoving", true);
    }

    private void AttackPlayer()
{
    if (attackCooldown <= 0f)
    {
        Vector2 direction = (player.position - transform.position).normalized;
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
        animator.SetTrigger("Attack");

        // Set cooldown after starting attack animation
        attackCooldown = isPhase2 ? attackCooldownPhase2 : attackCooldownPhase1;
    }
    else
    {
        attackCooldown -= Time.deltaTime;
    }

    animator.SetBool("IsMoving", false);
}

public void DealDamage() // Called by animation event
{
    if (player != null && player.TryGetComponent(out PlayerController playerController))
    {
        playerController.TakeDamage(damage);
        Debug.Log("Player takes damage!");
    }
}


    private bool IsPathBlocked(Vector2 targetPosition)
    {
        Vector2 direction = targetPosition - (Vector2)transform.position;
        float distance = direction.magnitude;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, obstacleLayer);

        return hit.collider != null; // Mengembalikan true jika ada penghalang
    }

    private void ActivatePhase2()
    {
        isPhase2 = true;
        Debug.Log("Fase kedua.");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Boss menerima damage. Sisa darah: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
            Destroy(gameObject);
        }
    }

    private void Die()
    {
        Debug.Log("Boss mati!");
        // animator.SetTrigger("Die");
        // Destroy(gameObject, 1f); // Hancurkan bos setelah animasi kematian
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
