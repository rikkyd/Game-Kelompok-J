using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // Ensure this namespace is included for UI elements

public class ArrowBarScript : MonoBehaviour
{
    private int arrowCount;
    public TextMeshProUGUI arrowText; // Reference to the UI Text component

    // Start is called before the first frame update
    void Start()
    {
        arrowCount = 10; // Initialize with a default value, for example 10
        UpdateArrowDisplay(); // Update the display at the start
    }

    // Update is called once per frame
    void Update()
    {
        // Example usage
        if (Input.GetKeyDown(KeyCode.B)) // Buy arrow
        {
            BuyArrow();
        }
    }

    public void UseArrow()
    {
        if (arrowCount > 0)
        {
            arrowCount--;
            Debug.Log("Arrow used. Remaining arrows: " + arrowCount);
            UpdateArrowDisplay(); // Update the display after using an arrow
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
        UpdateArrowDisplay(); // Update the display after buying an arrow
    }

    public int GetArrowCount()
    {
        return arrowCount;
    }

    public void OnFire(InputValue context)
    {
        if (context.isPressed)
        {
            UseArrow();
        }
    }

    // Method to update the Ammo display
    private void UpdateArrowDisplay()
    {
        if (arrowText != null)
        {
            arrowText.text = "Panah: " + arrowCount.ToString();
        }
    }
}