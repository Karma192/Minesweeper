using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTab : MonoBehaviour
{
    [SerializeField] int _height;
    [SerializeField] int _width;
    [SerializeField] int _nbMine;

    [SerializeField] GameObject safeTile;
    [SerializeField] GameObject mine;
    [SerializeField] GameObject data;
    [SerializeField] GameObject parent;

    [SerializeField] private GameObject[,] _board;

    public int Height { get => _height; }
    public int Width { get => _width; }
    public int Mine { get => _nbMine; }
    public GameObject[,] Board { get => _board; }

    public enum Tile
    {
        SAFE = 0,
        MINE = 1,
        SIZE = 1,
    }

    void Awake()
    {
        data = GameObject.Find("GameMaster");
        parent = GameObject.Find("Board");
    }

    void Start()
    {
        _height = data.GetComponent<GameData>().GetHeight;
        _width = data.GetComponent<GameData>().GetWidth;
        _nbMine = data.GetComponent<GameData>().GetMines;

        InitTab();
        parent.transform.position = new Vector2(0.5f - _width / 2, 0.5f - _height / 2);
    }

    void InitTab()
    {
        SetBoard();

        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                GameObject tile = Instantiate(_board[i,j], new Vector2(i * (int)Tile.SIZE, j * (int)Tile.SIZE), Quaternion.identity);
                tile.transform.parent = parent.transform;
            }
        }
    }

    private void SetBoard()
    {
        _board = new GameObject[_width, _height];

        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                _board[i, j] = safeTile;
            }
        }

        for (int i = 0; i < _nbMine; i++)
        {
            bool exit = false;

            while (!exit)
            {
                int x = Random.Range(0, _width);
                int y = Random.Range(0, _height);

                if (_board[x,y] == safeTile)
                {
                    _board[x,y] = mine;
                    exit = true;
                }
            }
        }
    }
}
