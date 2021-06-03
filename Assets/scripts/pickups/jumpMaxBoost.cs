using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpMaxBoost : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            pController.jumpMax++;
            Destroy(gameObject);
        }
    }
}
