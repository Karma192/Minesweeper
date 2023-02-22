using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private void OnMouseOver()
    {
        if (Input.GetButtonDown("Mouse0"))
        {
            if (gameObject.tag == "safe")
            {

            }

            if (gameObject.tag == "mine")
            {

            }
        }

        if (Input.GetButtonDown("Mouse1"))
        {
            GameObject child = Transform.GetChild(1).gameObject;
        }
    }
}
