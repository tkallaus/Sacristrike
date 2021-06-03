using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lobDestructor : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "destructable")
        {
            collision.gameObject.SendMessage("setPos", transform.position, SendMessageOptions.DontRequireReceiver);
            collision.gameObject.SendMessage("destroyCheck", new float[] { 1, GetComponent<CircleCollider2D>().radius }, SendMessageOptions.DontRequireReceiver);
        }
    }
}
