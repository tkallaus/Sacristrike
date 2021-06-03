using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum switchProtocol
{
    Laser
}
public class lobSwitch : MonoBehaviour
{
    public GameObject toBeSwitched;
    public switchProtocol objectProtocol;
    private int selected;
    public Sprite inactiveSprite;
    public Sprite activeSprite;
    public bool resetOnDisable;
    private void Start()
    {
        selected = (int)objectProtocol;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            switch (selected)
            {
                case 0:
                    toBeSwitched.GetComponent<laser>().lineActive = false;
                    break;
            }
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<SpriteRenderer>().sprite = activeSprite;
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
    private void OnDisable()
    {
        if (resetOnDisable)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<SpriteRenderer>().sprite = inactiveSprite;
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
