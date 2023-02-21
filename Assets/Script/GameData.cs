using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] private int height;
    [SerializeField] private  int width;
    [SerializeField] private int mines;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Height(string s){ height = int.Parse(s); }
    public void Width(string s) { width = int.Parse(s); }
    public void Mines(string s) { mines = int.Parse(s); }
}
