using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lobUnlock : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            pController.lobEnabled = true;
            Destroy(gameObject);
        }
    }
}
