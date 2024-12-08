using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public int maxHealth = 3;
    //tambahan dari rizza
    public int health = 3; // Health parameter to control death condition
    public float originalSpeed;
    private float freezetimer = 1f;
    private bool isFrozen = false;
    public int arrowCount = 10; // Initialize with a default value, for example 10
    public int coinCount = 0;
    public int potionCount = 0;
    public PlayerUI playerUI; // Reference to the PlayerUI component
    public SenjataPlayer senjataPlayer;

    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator animator;
    bool isAttacking;
    bool isDead;
    bool isShooting;

    private void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //tambahan dari rizza
        originalSpeed = moveSpeed;
        UpdateUI(); // Update the display at the start
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

    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset
        );

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnFire(InputValue context)
    {
        Debug.Log("OnFire called");
        if (context.isPressed && !isAttacking && !isDead && !isShooting && senjataPlayer != null)
        {
            Debug.Log("Fire action performed");
            UseArrow();
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
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetBool("isDeath", true);
        this.enabled = false; // Disable the player controller script
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
    }

    //tambahan dari rizza untuk boss buaya yang ngeluarin bola pasir hisap
    public void ModifySpeed(float slowAmount, float duration)
    {
        moveSpeed *= slowAmount;
        StartCoroutine(ResetSpeedAfterDuration(duration));
    }

    //tambahan untuk boss Harimau yang bikin ngefreeze
    public void Freeze(float duration)
    {
        if (!isFrozen) // Pastikan efek freeze tidak diaktifkan berulang kali
        {
            Debug.Log("Ngefreeze");
            isFrozen = true;
            originalSpeed = moveSpeed;
            moveSpeed = 0;
            StartCoroutine(UnfreezeAfterDuration(duration));
        }
    }

    private IEnumerator UnfreezeAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        Unfreeze();
    }

    private void Unfreeze()
    {
        isFrozen = false;
        moveSpeed = originalSpeed;
    }

    private IEnumerator ResetSpeedAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed;
    }

    public void UseArrow()
    {
        if (arrowCount > 0)
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
        arrowCount--;
        Debug.Log("Arrow used. Remaining arrows: " + arrowCount);
        UpdateUI(); // Update the display after using an arrow

        // Wait for the shooting animation to finish
        yield return new WaitForSeconds(1f); // Adjust the duration to match your animation

        isShooting = false;
    }

    public void BuyArrow()
    {
        arrowCount++;
        Debug.Log("Arrow bought. Total arrows: " + arrowCount);
        UpdateUI(); // Update the display after buying an arrow
    }

    public void CollectCoin()
    {
        coinCount++;
        Debug.Log("Coin collected. Total coins: " + coinCount);
        UpdateUI(); // Update the display after collecting a coin
    }

    public void CollectPotion()
    {
        potionCount++;
        Debug.Log("Potion collected. Total potions: " + potionCount);
        UpdateUI(); // Update the display after collecting a potion
    }

    private void UpdateUI()
    {
        if (playerUI != null)
        {
            playerUI.UpdateArrowDisplay(arrowCount);
            playerUI.UpdateCoinDisplay(coinCount);
            playerUI.UpdatePotionDisplay(potionCount);
        }
    }
}
