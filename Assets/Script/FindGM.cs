using UnityEngine;

public class FindGM : MonoBehaviour
{
    private GameObject _gameMaster;

    private void Start()
    {
        _gameMaster = GameObject.Find("GameMaster");
    }

    public void GetHeight(int value)
    {
        _gameMaster.GetComponent<GameData>().GetHeight = value;
    }

    public void GetWidth(int value)
    {
        _gameMaster.GetComponent<GameData>().GetWidth = value;
    }

    public void GetMines(int value)
    {
        _gameMaster.GetComponent<GameData>().GetMines = value;
    }

    public void Height(string s) { _gameMaster.GetComponent<GameData>().Height(s); }
    public void Width(string s) { _gameMaster.GetComponent<GameData>().Width(s); }
    public void Mines(string s) { _gameMaster.GetComponent<GameData>().Mines(s); }

    public void ChangeScene(string name)
    {
        _gameMaster.GetComponent<SceneMaster>().ChangeScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
