using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutsceneStartTrig : MonoBehaviour
{
    private bool playerIsHere = false;
    public cutsceneTemplate startingScene;
    public bool activeOnContact = false;

    void Update()
    {
        if (activeOnContact)
        {
            if (playerIsHere)
            {
                startingScene.gameObject.SetActive(true);
                GetComponent<BoxCollider2D>().enabled = false;
                playerIsHere = false;
            }
        }
        else if (playerIsHere && Input.GetButtonDown("Fire1"))
        {
            startingScene.gameObject.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
            playerIsHere = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            playerIsHere = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            playerIsHere = false;
        }
    }
}
