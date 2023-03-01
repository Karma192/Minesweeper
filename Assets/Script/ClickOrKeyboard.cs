using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ClickOrKeyboard : MonoBehaviour
{
    [SerializeField] GameObject button;
    [SerializeField] Text text;
    private GameObject data;
    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.Find("GameMaster");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        data.GetComponent<GameData>().KeyboardMode = !data.GetComponent<GameData>().KeyboardMode;
        if (!data.GetComponent<GameData>().KeyboardMode)
        {
            button.transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,80);
            text.text = "Click";
        } else
        {
            button.transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120);
            text.text = "Keyboard";
        }
    }
}
