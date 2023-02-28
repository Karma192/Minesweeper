using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static GameData instance;
    private GameData() { }

    [SerializeField] private int _height;
    [SerializeField] private int _width;
    [SerializeField] private int _mines;
    [SerializeField] private bool _lose;
    [SerializeField] private bool _win;
    [SerializeField] public Sprite[] _number = new Sprite[9];
    [SerializeField] public Sprite _bomb, _flag, _selected;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetHeight { get => _height; set => _height = value; }
    public int GetWidth { get => _width; set => _width = value; }
    public int GetMines { get => _mines; set => _mines = value; }
    public bool Lose { get => _lose; set => _lose = value; }
    public bool Win { get => _win; set => _win = value; }
    public Sprite GetNumber(int number)
    {
        return _number[number];
    }

    public void Height(string s) { _height = int.Parse(s); }
    public void Width(string s) { _width = int.Parse(s); }
    public void Mines(string s) { _mines = int.Parse(s); }
}
