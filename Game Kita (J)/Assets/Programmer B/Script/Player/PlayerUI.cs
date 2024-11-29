using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI arrowText; // Reference to the UI Text component for arrows
    public TextMeshProUGUI coinText;  // Reference to the UI Text component for coins
    public TextMeshProUGUI potionText; // Reference to the UI Text component for potions
    public Slider HPBar; // Reference to the UI Slider component for health
    // Method to update the arrow display

    public void UpdateUI(int hp, int maxHp, int arrowCount, int coinCount, int potionCount)
    {
        UpdateHPBar(hp, maxHp);
        UpdateArrowDisplay(arrowCount);
        UpdateCoinDisplay(coinCount);
        UpdatePotionDisplay(potionCount);
    }

    public void UpdateHPBar(int currentHealth, int maxHealth)
    {
        if (HPBar != null)
        {
            HPBar.maxValue = maxHealth;
            HPBar.value = currentHealth;
        }
    }
    public void UpdateArrowDisplay(int arrowCount)
    {
        if (arrowText != null)
        {
            arrowText.text = arrowCount.ToString();
        }
    }

    // Method to update the coin display
    public void UpdateCoinDisplay(int coinCount)
    {
        if (coinText != null)
        {
            coinText.text = coinCount.ToString();
        }
    }

    // Method to update the potion display
    public void UpdatePotionDisplay(int potionCount)
    {
        if (potionText != null)
        {
            potionText.text = potionCount.ToString();
        }
    }
}