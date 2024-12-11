using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public PlayerStatsSO playerStats;
    public int coinValue = 1;
    public int arrowValue = 1;
    public int potionValue = 1;

    private void OnTriggerEnter2D(Collider2D col)
    {
        PlayerInventory playerInventory = col.GetComponent<PlayerInventory>();
        if (col.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Coin"))
            {
                playerInventory.CollectCoin();
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("Arrow"))
            {
                playerInventory.CollectArrow();
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("Potion"))
            {
                playerInventory.CollectPotion();
                Destroy(gameObject);
            }
        }
    }
}