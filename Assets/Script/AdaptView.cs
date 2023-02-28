using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptView : MonoBehaviour
{
    void Start()
    {
        GameObject gm = GameObject.Find("GameMaster");
        GetComponent<Camera>().orthographicSize = gm.GetComponent<GameData>().GetHeight / 2;
    }
}
