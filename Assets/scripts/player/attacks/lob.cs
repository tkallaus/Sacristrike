using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lob : MonoBehaviour
{
    private int hitID;
    public int knockback;
    public GameObject explosion;
    private void Start()
    {
        Destroy(gameObject, 5);
        hitID = Random.Range(int.MinValue, int.MaxValue);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            if (!collision.collider.isTrigger)
            {
                collision.gameObject.SendMessage("TakeDamage", new int[] { 1, hitID, 1, knockback }, SendMessageOptions.DontRequireReceiver);
            }
        }
        if(collision.gameObject.tag == "platform")
        {
            if(collision.GetContact(0).normal.y >= 0.9f)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
        else
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
