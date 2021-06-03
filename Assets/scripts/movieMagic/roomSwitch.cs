using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomSwitch : MonoBehaviour
{
    public static bool deadSwitch = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!pController.pcDead)
        {
            if (collision.gameObject.layer == 31)
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!pController.pcDead)
        {
            if (collision.gameObject.layer == 31)
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }
    private void Update()
    {
        if (deadSwitch)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            deadSwitch = false;
        }
    }
}
