using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string s)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(s));
    }
}
