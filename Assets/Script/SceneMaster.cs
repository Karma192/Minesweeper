using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneMaster : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
