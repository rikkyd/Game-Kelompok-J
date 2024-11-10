using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public int health = 3; // Health parameter to control death condition
    public int arrowCount = 10; // Initialize with a default value, for example 10
    public int coinCount = 0;
    public int potionCount = 0;
    public PlayerUI playerUI; // Reference to the PlayerUI component

    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator animator;
    bool isAttacking;
    bool isDead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        if (context.isPressed)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // Ensure your enemy objects have the tag "Enemy"
        {
            TakeDamage(1); // Adjust the damage value as needed
        }
    }

    public void UseArrow()
    {
        if (arrowCount > 0)
        {
            arrowCount--;
            Debug.Log("Arrow used. Remaining arrows: " + arrowCount);
            UpdateUI(); // Update the display after using an arrow
        }
        else
        {
            Debug.Log("No arrows left to use.");
        }
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