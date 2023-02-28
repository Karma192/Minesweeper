using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneMaster : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
