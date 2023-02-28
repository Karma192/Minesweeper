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
    [SerializeField] private int _nbBomb = 1;

    [SerializeField] private GameObject _gameMaster;
    [SerializeField] private GameObject _child;

    [SerializeField] private List<GameObject> _neighbor;

    private void Start()
    {
        _child = gameObject.transform.GetChild(0).gameObject;
        _gameMaster = GameObject.Find("GameMaster");
        GetNeighborMine();
    }

    private void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!_flagged && !_clicked && !_gameMaster.GetComponent<GameData>().Lose)
            {
                GameObject.Find("BoardManager").gameObject.GetComponent<CreateTab>()._firstClick = true;
                GetComponent<SpriteRenderer>().sprite = _gameMaster.GetComponent<GameData>()._selected;

                if (gameObject.CompareTag("safe"))
                {                    
                    Reveal();
                }

                if (gameObject.CompareTag("mine"))
                {
                    _child.GetComponent<SpriteRenderer>().sprite = _gameMaster.GetComponent<GameData>()._bomb;
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
                _child.GetComponent<SpriteRenderer>().sprite = _gameMaster.GetComponent<GameData>()._flag;
                _flagged = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _neighbor.Add(collision.gameObject);
    }

    private void Reveal()
    {
        _child.GetComponent<SpriteRenderer>().sprite = _gameMaster.GetComponent<GameData>().GetNumber(_nbBomb);
        _child.SetActive(true);
        if (_nbBomb == 0)
        {
            CheckReveal();
        }
        _clicked = true;
    }

    private void CheckReveal()
    {
        if (_neighbor.Count != 0 && !_clicked)
        {
            for (int i = 0; i < _neighbor.Count; i++)
            {
                if (!_neighbor[i].transform.CompareTag("mine"))
                {
                    _neighbor[i].GetComponent<Interact>().Reveal();
                }
            }
        }
    }

    private void GetNeighborMine()
    {
        if (!CompareTag("mine"))
        {
            for (int i = 0; i < _neighbor.Count; i++)
            {
                if (_neighbor[i].transform.CompareTag("mine"))
                {
                    _nbBomb++;
                }
            }
        }
    }
}
