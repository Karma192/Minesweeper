using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] private int _height;
    [SerializeField] private int _width;
    [SerializeField] private int _mines;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public int GetHeight { get => _height; set => _height = value; }
    public int GetWidth { get => _width; set => _width = value; }
    public int GetMines { get => _mines; set => _mines = value; }

    public void Height(string s){ _height = int.Parse(s); }
    public void Width(string s) { _width = int.Parse(s); }
    public void Mines(string s) { _mines = int.Parse(s); }
}
