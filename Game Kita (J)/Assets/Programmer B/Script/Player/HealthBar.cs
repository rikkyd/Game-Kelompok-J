using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar; // Make it private with SerializeField
    private PlayerController playerController;

    void Start()
    {
        // Try to get the Slider component if it's not assigned
        if (healthBar == null)
        {
            healthBar = GetComponent<Slider>();
        }

        // Try to find the player if not assigned
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
        if (playerController != null && healthBar != null)
        {
            healthBar.maxValue = playerController.maxHealth;
            healthBar.value = playerController.health;
        }
        else
        {
            Debug.LogError("HealthBar: Missing required references. Please check Player tag and Slider component.");
        }
    }

    public void SetHealth(int hp)
    {
        if (healthBar != null)
        {
            healthBar.value = Mathf.Clamp(hp, 0, healthBar.maxValue);
        }
        else
        {
            Debug.LogError("HealthBar: Slider reference is missing!");
        }
    }
}