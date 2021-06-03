using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reflected : MonoBehaviour
{
    private int knockback;
    private int hitID;
    private void Start()
    {
        knockback = 3;
        hitID = Random.Range(int.MinValue, int.MaxValue);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(gameObject.layer == 10)
        {
            if (collision.gameObject.layer == 11)
            {
                if (!collision.isTrigger)
                {
                    collision.gameObject.SendMessage("TakeDamage", new int[] { 1, hitID, 0, knockback }, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
