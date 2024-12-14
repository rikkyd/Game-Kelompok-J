using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseManager : MonoBehaviour
{
    public WinLoseManager instance;
    public GameObject winPanel;
    public GameObject losePanel;
    public TextMeshProUGUI coinWin;
    public TextMeshProUGUI coinLose;
    public PlayerStatsSO playerStats;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        winPanel.SetActive(false);
        losePanel.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        coinWin.text = playerStats.coinCount.ToString();
        coinLose.text = playerStats.coinCount.ToString();
    }

    public void Win()
    {
        winPanel.SetActive(true);
        losePanel.SetActive(false);
        coinWin.text = playerStats.coinCount.ToString();
    }

    public void Lose()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(true);
        coinLose.text = playerStats.coinCount.ToString();
    }

    public void NextStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
