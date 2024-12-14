using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class StoreManager : MonoBehaviour
{
    public static StoreManager instance;
    [Header("Upgrade Bar")]
    public Slider skillBar;
    public Slider speedBar;
    public Slider potionBar;
    [Header("Upgrade Levels")]
    public TextMeshProUGUI skillLevel;
    public TextMeshProUGUI speedLevel;
    public TextMeshProUGUI potionCount;
    [Header("Upgrade Button")]
    public Button upgradeSkillBtn;
    public Button upgradeSpeedBtn;
    public Button buyPotionBtn;
    public GameObject resetSkillBtn;
    public GameObject resetSpeedBtn;

    [Header("Upgrade Cost")]
    public TextMeshProUGUI skillCost;
    public TextMeshProUGUI speedCost;
    public TextMeshProUGUI potionCost;
    
    [Header("Information button")]
    public Button skillInfoBtn;
    public Button speedInfoBtn;
    public Button potionInfoBtn;

    [Header("Information Panel")]
    public GameObject skillInfoPanel;
    public GameObject speedInfoPanel;
    public GameObject potionInfoPanel;
    public Button backBtn;
    public Button outsideClickButton; // Reference to the transparent button
    public TextMeshProUGUI playerCoins;
    public PlayerStatsSO playerStats;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        skillInfoPanel.SetActive(false);
        speedInfoPanel.SetActive(false);
        potionInfoPanel.SetActive(false);
        outsideClickButton.gameObject.SetActive(false); // Hide the button initially
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        if (playerCoins != null)
        {
            playerCoins.text = playerStats.coinCount.ToString();
        }

        if (skillBar != null && speedBar != null && potionBar != null)
        {
            skillBar.value = playerStats.skillUpgrade;
            speedBar.value = playerStats.speedUpgrade;
            potionBar.value = playerStats.potionCount;
        }

        if (skillLevel != null && speedLevel != null && potionCount != null)
        {
            skillLevel.text = playerStats.skillUpgrade >= 5 
                ? "Level Max"
                : "Level " + playerStats.skillUpgrade.ToString();

            speedLevel.text = playerStats.speedUpgrade >= 5
                ? "Level Max"
                : "Level " + playerStats.speedUpgrade.ToString();

            potionCount.text = playerStats.potionCount >= 5
                ? "Max Ammount"
                : "Level " + playerStats.potionCount.ToString();
        }

        if (skillCost != null && speedCost != null && potionCost != null)
        {
            skillCost.text = (playerStats.skillUpgrade + 1).ToString();
            speedCost.text = (playerStats.speedUpgrade + 1).ToString();
            potionCost.text = (playerStats.potionCount + 1).ToString();
        }

        if (playerStats.skillUpgrade > 0)
        {
            resetSkillBtn.SetActive(true);
        }
        else
        {
            resetSkillBtn.SetActive(false);
        }

        if(playerStats.speedUpgrade > 0)
        {
            resetSpeedBtn.SetActive(true);
        }
        else
        {
            resetSpeedBtn.SetActive(false);
        }
    }

    public void UpgradeSkillButton()
    {
        if (playerStats.skillUpgrade < 5)
        {
            if (playerStats.coinCount >= playerStats.skillUpgrade + 1)
            {
                playerStats.coinCount -= playerStats.skillUpgrade + 1;
                playerStats.skillUpgrade++;
                playerStats.maxArrowCount = playerStats.baseArrowCount + playerStats.skillUpgrade * 5;
            }
        }
        else
        {
            Debug.Log("Max Level Reached");
        }
    }

    public void UpgradeSpeedButton()
    {
        if (playerStats.speedUpgrade < 5)
        {
            if (playerStats.coinCount >= playerStats.speedUpgrade + 1)
            {
                playerStats.coinCount -= playerStats.speedUpgrade + 1;
                playerStats.speedUpgrade++;
                playerStats.moveSpeed = playerStats.baseMoveSpeed + playerStats.speedUpgrade;
            }
        }
        else
        {
            Debug.Log("Max Level Reached");
        }  
    }

    public void BuyPotionButton()
    {
        if (playerStats.potionCount < playerStats.maxPotionCount)
        {
            if (playerStats.coinCount >= playerStats.potionCount + 1)
            {
                playerStats.coinCount -= playerStats.potionCount + 1;
                playerStats.potionCount++;
            }
        }
        else{
            Debug.Log("Max Potion Count Reached");
        }
    }

    public void ResetSkill()
    {
        int totalRefund = 0;
        for (int i = 1; i <= playerStats.skillUpgrade; i++)
        {
            totalRefund += i;
        }
        playerStats.coinCount += totalRefund;
        playerStats.skillUpgrade = 0;
        playerStats.maxArrowCount = playerStats.baseArrowCount;
    }

    public void ResetSpeed()
    {
        int totalRefund = 0;
        for (int i = 1; i <= playerStats.speedUpgrade; i++)
        {
            totalRefund += i;
        }
        playerStats.coinCount += totalRefund;
        playerStats.speedUpgrade = 0;
        playerStats.moveSpeed = playerStats.baseMoveSpeed;
    }

    public void SkillInfoBtn()
    {
        skillInfoPanel.SetActive(true);
        speedInfoPanel.SetActive(false);
        potionInfoPanel.SetActive(false);
        outsideClickButton.gameObject.SetActive(true); // Show the button
    }

    public void SpeedInfoBtn()
    {
        skillInfoPanel.SetActive(false);
        speedInfoPanel.SetActive(true);
        potionInfoPanel.SetActive(false);
        outsideClickButton.gameObject.SetActive(true); // Show the button
    }

    public void PotionInfoBtn()
    {
        skillInfoPanel.SetActive(false);
        speedInfoPanel.SetActive(false);
        potionInfoPanel.SetActive(true);
        outsideClickButton.gameObject.SetActive(true); // Show the button
    }

    public void OutsideClick()
    {
        skillInfoPanel.SetActive(false);
        speedInfoPanel.SetActive(false);
        potionInfoPanel.SetActive(false);
        outsideClickButton.gameObject.SetActive(false); // Hide the button
    }

    public void BackBtn()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
