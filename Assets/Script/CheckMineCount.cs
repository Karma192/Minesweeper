using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckMineCount : MonoBehaviour
{
    [SerializeField] private InputField _height;
    [SerializeField] private InputField _width;

    [SerializeField]
    private void Update()
    {
        int actual = int.Parse(GetComponent<InputField>().text);
        int h = int.Parse(_height.text) - 1;
        int w = int.Parse(_width.text) - 1;

        if (actual > (h * w))
        {
            int x = (h * w);
            GetComponent<InputField>().text = x.ToString();
        }
    }
}
