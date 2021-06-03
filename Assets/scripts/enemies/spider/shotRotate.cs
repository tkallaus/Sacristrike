using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotRotate : MonoBehaviour
{
    private float randInt;
    private void Start()
    {
        Destroy(gameObject, 10);
        randInt = Random.Range(0, 2)*2-1;
    }
    private void FixedUpdate()
    {
        transform.Rotate(0, 0, randInt);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag != "platform")
        {
            if(collision.gameObject.layer != 10 && collision.gameObject.layer != 13)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
