using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeInTrigger : MonoBehaviour
{
    public fadein f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (!pController.pcDead)
            {
                f.active = true;
            }
        }
    }
}
