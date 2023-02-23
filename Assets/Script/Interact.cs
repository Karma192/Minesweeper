using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] Sprite _flag;
    [SerializeField] Sprite _bomb;
    [SerializeField] Sprite _selected;

    [SerializeField] private bool _flagged;
    [SerializeField] private bool _clicked;
    [SerializeField] private int _nbBomb;
    [SerializeField] private GameObject _gameMaster;
    [SerializeField] private GameObject _child;
    [SerializeField] private GameObject _text;

    [SerializeField] private List<GameObject> _neighbor;

    private void Start()
    {
        _child = gameObject.transform.GetChild(0).gameObject;
        _text = gameObject.transform.GetChild(1).gameObject;
        _gameMaster = GameObject.Find("GameMaster");
        GetNeighbor();
    }

    private void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!_flagged && !_clicked)
            {
                if (gameObject.tag == "safe")
                {
                    _clicked = true;
                    Reveal();
                }

                if (gameObject.tag == "mine")
                {
                    GetComponent<SpriteRenderer>().sprite = _selected;
                    _child.GetComponent<SpriteRenderer>().sprite = _bomb;
                    _child.SetActive(true);
                    _gameMaster.GetComponent<GameData>().Lose = true;
                }
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (_flagged)
            {
                _child.SetActive(false);
                _flagged = false;
            }
            else
            if (!_clicked)
            {
                _child.SetActive(true);
                _child.GetComponent<SpriteRenderer>().sprite = _flag;
                _flagged = true;
            }
        }
    }

    private void Reveal()
    {
        GetComponent<SpriteRenderer>().sprite = _selected;
        _child.GetComponent<SpriteRenderer>().sprite = null;
        _child.SetActive(true);
        if (_nbBomb == 0)
        {
            CheckReveal();
        }
        else
        {
            _text.GetComponent<TextMeshPro>().text = _nbBomb.ToString();
        }
    }

    private void CheckReveal()
    {
        for (int i = 0; i < _neighbor.Count; i++)
        {
            if (_neighbor[i].transform.tag != "mine")
            {
                _neighbor[i].GetComponent<Interact>().Reveal();
            }
        }
    }

    private void GetNeighbor()
    {
        GameObject[,] board = GameObject.Find("BoardManager").GetComponent<CreateTab>().Board;
        int posx = 0;
        int posy = 0;

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] = gameObject)
                {
                    posx = i;
                    posy = j;
                    break;
                }
            }
        }

        for (int i = posx - 1; i < posx + 1; i++)
        {
            for (int j = posy - 1; j < posy + 1; j++)
            {
                _neighbor.Add(board[i, j]);
            }
        }

        Debug.Log(_neighbor.Count);
    }
}
