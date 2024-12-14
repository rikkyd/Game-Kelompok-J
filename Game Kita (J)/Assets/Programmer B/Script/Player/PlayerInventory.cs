using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public PlayerStatsSO playerStats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectArrow()
    {
        if (playerStats.arrowCount <= playerStats.maxArrowCount){
            playerStats.arrowCount++;
            Debug.Log("Arrow collected. Total arrow: " + playerStats.arrowCount);
        }
        else
        {
            Debug.Log("Arrow count is at maximum.");
        }
    }

    public void CollectCoin()
    {
        playerStats.coinCount++;
        Debug.Log("Coin collected. Total coins: " + playerStats.coinCount);
    }

    public void CollectPotion()
    {
        if (playerStats.potionCount <= playerStats.maxPotionCount){
            playerStats.potionCount++;
            Debug.Log("Potion collected. Total potions: " + playerStats.potionCount);
        }
        else
        {
            Debug.Log("Potion count is at maximum.");
        }
    }

    public void usePotion()
    {
        if (playerStats.potionCount > 0)
        {
            if (playerStats.health < 5)
            {
                playerStats.health++;
                Debug.Log("Potion used. Health increased to: " + playerStats.health);
            }
            else
            {
                Debug.Log("Health is already at maximum. Potion used, but no effect.");
            }
            playerStats.potionCount--;
            Debug.Log("Remaining potions: " + playerStats.potionCount);
        }
        else
        {
            Debug.Log("No potions left to use.");
        }
    }
}
