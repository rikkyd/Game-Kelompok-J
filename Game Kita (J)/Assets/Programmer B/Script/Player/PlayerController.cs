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
    public int health = 3;


    //tambahan dari rizza
    public float originalSpeed;
    private float freezetimer = 1f;
    private bool isFrozen = false;

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

        //tambahan dari rizza
        originalSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        if (isAttacking)
        {
            // During an attack, the player won't move
            return;
        }

        bool isMoving = movementInput != Vector2.zero;

        // Try to move in the direction of input if there is movement
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

            // Update animator direction parameters
            animator.SetFloat("DirectionX", movementInput.x);
            animator.SetFloat("DirectionY", movementInput.y);
        }

        // Update the isWalking parameter in the Animator
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

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnAttack(InputValue attackValue)
    {
        if (attackValue.isPressed && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);

        // Delay for the duration of the attack animation (adjust this duration)
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

}
