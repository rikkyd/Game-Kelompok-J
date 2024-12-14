using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    [Header("Shooting Settings")]
    public float startTimeBtwShots;
    private float timeBtwShots;

    public PlayerUI playerUI; // Reference to the PlayerUI component
    public SenjataPlayer senjataPlayer;
    public PlayerStatsSO playerStats;

    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    PlayerInventory playerInventory;
    Animator animator;
    bool isAttacking;
    bool isDead;
    bool isShooting;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInventory = GetComponent<PlayerInventory>();
        playerStats.arrowCount = playerStats.maxArrowCount; // Initialize the arrow count
        playerStats.health = playerStats.maxHealth; // Initialize the health
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        if (isAttacking)
        {
            return;
        }

        bool isMoving = movementInput != Vector2.zero;

        if (isMoving)
        {
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }

            animator.SetFloat("DirectionX", movementInput.x);
            animator.SetFloat("DirectionY", movementInput.y);
        }

        animator.SetBool("isWalking", isMoving);

        // Update the UI
        playerUI.UpdateUI(playerStats.health, playerStats.maxHealth, playerStats.arrowCount, playerStats.coinCount, playerStats.potionCount);
    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            playerStats.moveSpeed * Time.fixedDeltaTime + collisionOffset
        );

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * playerStats.moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }

        private void Update()
    {
        // Update the cooldown timer for shooting
        if (timeBtwShots > 0)
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void OnFire(InputValue context)
    {
        Debug.Log("OnFire called");
        if (context.isPressed && !isAttacking && !isDead && !isShooting && timeBtwShots <= 0 && senjataPlayer != null)
        {
            Debug.Log("Fire action performed");
            UseArrow();
        }
    }

    public void OnHeal(InputValue context)
    {
        if (context.isPressed && !isAttacking && !isDead && !isShooting && playerStats.potionCount > 0)
        {
            playerInventory.usePotion();
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnAttack(InputValue attackValue)
    {
        if (attackValue.isPressed && !isAttacking && !isDead)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);

        yield return new WaitForSeconds(0.5f);

        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }

    public void TakeDamage(int damage)
    {
        playerStats.health -= damage;
        if (playerStats.health <= 0)
        {
            playerStats.health = 0;
            playerUI.UpdateUI(playerStats.health, playerStats.maxHealth, playerStats.arrowCount, playerStats.coinCount, playerStats.potionCount);
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetBool("isDeath", true);
        this.enabled = false; // Disable the player controller script
    }

    public void OnDeathAnimationEnd()
    {
        SceneManager.LoadScene("StageSelection");
    }

    // Method to detect collision with enemy
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // Ensure your enemy objects have the tag "Enemy"
        {
            Debug.Log("kena damage");
            TakeDamage(1); // Adjust the damage value as needed
        }

        //tambahan dari rizza
        if (other.CompareTag("Trap"))
        {
            Debug.Log("Kena Damage dari trap");
            TakeDamage(1);
        }

        if (other.CompareTag("ProjectileKomodo"))
        {
            TakeDamage(2);
        }
    }

    public void UseArrow()
    {
        if (playerStats.arrowCount > 0)
        {
            StartCoroutine(ShootArrow());
        }
        else
        {
            Debug.Log("No arrows left to use.");
        }
    }

    private IEnumerator ShootArrow()
    {
        isShooting = true;
        senjataPlayer.Shoot();
        playerStats.arrowCount--;
        Debug.Log("Arrow used. Remaining arrows: " + playerStats.arrowCount);

        yield return null; // Ensure the method yields a value

        isShooting = false;
        timeBtwShots = startTimeBtwShots; // Reset the cooldown timer
    }
}
