using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI arrowText; // Reference to the UI Text component for arrows
    public TextMeshProUGUI coinText;  // Reference to the UI Text component for coins
    public TextMeshProUGUI potionText; // Reference to the UI Text component for potions

    // Method to update the arrow display
    public void UpdateArrowDisplay(int arrowCount)
    {
        if (arrowText != null)
        {
            arrowText.text = "Panah: " + arrowCount.ToString();
        }
    }

    // Method to update the coin display
    public void UpdateCoinDisplay(int coinCount)
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount.ToString();
        }
    }

    // Method to update the potion display
    public void UpdatePotionDisplay(int potionCount)
    {
        if (potionText != null)
        {
            potionText.text = "Potions: " + potionCount.ToString();
        }
    }
}