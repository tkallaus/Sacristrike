using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpMaxBoost : MonoBehaviour
{
    public AudioClip itemGet;
    public bool SetJumpMax = false;
    public int NumToSetTo = 0;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (SetJumpMax)
            {
                pController.jumpMax = NumToSetTo;
            }
            else
            {
                pController.jumpMax++;
            }
            AudioSource.PlayClipAtPoint(itemGet, transform.position);
            Destroy(gameObject);
        }
    }
}
