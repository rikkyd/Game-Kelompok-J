using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStage1 : MonoBehaviour
{
    public string sceneLoad;
    public string enemyTag;

    private bool isActive = false;

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        if (enemies.Length == 0 && !isActive)
        {
            NextFloor();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player") && isActive)
        {
            ChangetoMenu();
        }
        else if(col.CompareTag("Player") && !isActive)
        {
            Debug.Log("Kalahkan musuh dulu coy");
        }
    }

    private void NextFloor()
    {
        isActive = true;
        Debug.Log("bisa pindah scene");
    }

    private void ChangetoMenu()
    {
        SceneManager.LoadScene(sceneLoad);
    }
}
