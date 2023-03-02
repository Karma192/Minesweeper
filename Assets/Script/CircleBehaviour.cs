using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBehaviour : MonoBehaviour
{
    [SerializeField] Sprite clicksprite;
    Sprite defaultSprite;
    [SerializeField] int speed = 5;
    private GameObject data;
    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.Find("GameMaster");
        defaultSprite = transform.GetComponent<SpriteRenderer>().sprite;
        if (data.GetComponent<GameData>().KeyboardMode)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        } else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (data.GetComponent<GameData>().KeyboardMode)
        {
            if (Input.GetButton("Jump"))
            {
                transform.GetComponent<SpriteRenderer>().sprite = clicksprite;
            }
            else
            {
                transform.GetComponent<SpriteRenderer>().sprite = defaultSprite;
            }

            float horizontal = Input.GetAxis("Horizontal"); // Horizontal axis
            float vertical = Input.GetAxis("Vertical"); // Vertical axis

            Vector3 movement = new(horizontal, vertical);

            transform.position += speed * Time.deltaTime * movement;
        }
        
    }
}
