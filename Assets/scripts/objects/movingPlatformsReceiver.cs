using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatformsReceiver : MonoBehaviour
{
    public int journeyPosition;
    public bool pCollided = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            pCollided = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            pCollided = false;
        }
    }
}
