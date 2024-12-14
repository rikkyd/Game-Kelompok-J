using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnActivation : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene("Stage1", LoadSceneMode.Single);
    }
}