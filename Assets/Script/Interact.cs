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
    [SerializeField] private int _nbBomb;
    [SerializeField] private GameObject _gameMaster;
    [SerializeField] private GameObject _child;
    [SerializeField] private GameObject _text;

    private void Start()
    {
        _child = gameObject.transform.GetChild(0).gameObject;
        _text = gameObject.transform.GetChild(1).gameObject;
        _gameMaster = GameObject.Find("GameMaster");
    }

    private void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!_flagged)
            {
                if (gameObject.tag == "safe")
                {
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
        _text.GetComponent<TextMeshPro>().text = _nbBomb.ToString();
        CheckReveal();
    }

    private void CheckReveal()
    {
        GameObject bm = GameObject.Find("BoardManager");
        GameObject[,] board = bm.GetComponent<CreateTab>().Board;
        int posx = (int)transform.localPosition.x - 1;
        int posy = (int)transform.localPosition.x - 1;

        for (int i = posx; i < posx + 3; i++)
        {
            for (int j = posy; j < posy + 3; j++)
            {
                if (board[i,j].transform.tag != "mine")
                {
                    if (board[])
                    Reveal();
                }
            }
        }
    }
}
