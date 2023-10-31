﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lobUnlock : MonoBehaviour
{
    public bool superlobUnlock = false;

    public AudioClip itemGet;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (superlobUnlock)
            {
                pController.SUPERLOB = true;
            }
            pController.lobEnabled = true;
            AudioSource.PlayClipAtPoint(itemGet, transform.position);
            Destroy(gameObject);
        }
    }
}
