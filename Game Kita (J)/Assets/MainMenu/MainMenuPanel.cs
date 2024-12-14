using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenuPanel : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Store()
    {
        SceneManager.LoadScene("StoreScene", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Chapter2()
    {
        SceneManager.LoadScene("Stage2", LoadSceneMode.Single);
    }

    public void Chapter3()
    {
        SceneManager.LoadScene("Stage3", LoadSceneMode.Single);
    }

    public void Chapter4()
    {
        SceneManager.LoadScene("Stage4", LoadSceneMode.Single);
    }

}