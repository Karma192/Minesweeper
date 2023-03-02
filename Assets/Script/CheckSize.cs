using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckSize : MonoBehaviour
{
    private void Update()
    {
        int x = int.Parse(GetComponent<InputField>().text);
        if (x > 50)
        {
            GetComponent<InputField>().text = 50.ToString();
        }
    }
}
