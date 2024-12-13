using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropItem
{
    public GameObject itemPrefab; // Prefab of the item
    public int dropAmount; // Amount of the item to drop
}

public class EnemyDropItem : MonoBehaviour
{
    [Header("Drop Settings")]
    public List<DropItem> dropItems; // List to hold the drop items and their amounts

    public void DropItem()
    {
        // Iterate through each drop item in the list
        foreach (var dropItem in dropItems)
        {
            // Check if the prefab is not null
            if (dropItem.itemPrefab != null)
            {
                // Drop the item the specified number of times
                for (int i = 0; i < dropItem.dropAmount; i++)
                {
                    Instantiate(dropItem.itemPrefab, transform.position, Quaternion.identity);
                    Debug.Log($"Item {dropItem.itemPrefab.name} dropped {dropItem.dropAmount} times!");
                }
            }
            else
            {
                Debug.LogWarning("Item prefab is not set!");
            }
        }
    }

    private void OnDestroy()
    {
        // Ensure items are only dropped if the enemy is destroyed in the game
        if (gameObject.scene.isLoaded) // Avoid dropping when exiting Play Mode
        {
            DropItem();
        }
    }
}