using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetVolume(int vol)
    {
        int count = gameObject.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<AudioSource>().volume = vol;
        }
    }
}
