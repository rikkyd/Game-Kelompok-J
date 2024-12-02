using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTrap : MonoBehaviour
{
    public float slowDuration = 1.5f;
    public float slowAmount = 0.4f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SlowMovement(other);

        }
    }

    void SlowMovement(Collider2D player)
    {
        Debuff playermovement = player.GetComponent<Debuff>();

        if (playermovement != null)
        {
            playermovement.ModifySpeed(slowAmount, slowDuration);
        }
    }
}
