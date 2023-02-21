using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    public void ChangeScene(Scene s)
    {
        SceneManager.SetActiveScene(s);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
