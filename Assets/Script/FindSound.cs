using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _music = new AudioClip[4];
    [SerializeField] private GameObject _sound;
    [SerializeField] private float _volume = 0.3f;

    private void Start()
    {
        _sound = GameObject.Find("Sound");
    }

    private void Update()
    {
        _sound.transform.GetChild(0).GetComponent<AudioSource>().volume = _volume;
    }

    public void ChangeBGM(int index)
    {
        _sound.transform.GetChild(0).GetComponent<AudioSource>().clip = _music[index];
    }

    public void PlayBGM()
    {
        _sound.transform.GetChild(0).GetComponent<AudioSource>().Play();
    }

    public float ChangeVolume
    {
        set => _volume = value;
    }
}
