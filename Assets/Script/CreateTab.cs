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

    enum Tile
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

    // Start is called before the first frame update
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
        int[,] board = SetBoard();

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for(int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i,j] == (int)Tile.MINE)
                {
                    GameObject tile = Instantiate(mine, new Vector2(i * (int)Tile.SIZE, j * (int)Tile.SIZE), Quaternion.identity);
                    tile.transform.parent = parent.transform;
                } else if (board[i,j] == (int)Tile.SAFE)
                {
                    GameObject tile = Instantiate(safeTile, new Vector2(i * (int)Tile.SIZE, j * (int)Tile.SIZE), Quaternion.identity);
                    tile.transform.parent = parent.transform;
                }
            }
        }
    }

    int[,] SetBoard()
    {
        int[,] tmp = new int[_width, _height];

        for (int i = 0; i < tmp.GetLength(0)-1; i++)
        {
            for (int j = 0; j < tmp.GetLength(1)-1; j++)
            {
                tmp[i, j] = (int)Tile.SAFE;
            }
        }

        for (int i = 0; i < _nbMine; i++)
        {
            bool exit = false;

            while (!exit)
            {
                int x = Random.Range(0, _width);
                int y = Random.Range(0, _height);

                if (tmp[x,y] == (int)Tile.SAFE)
                {
                    tmp[x,y] = (int)Tile.MINE;
                    exit = true;
                }
            }
        }

        return tmp;
    }
}
