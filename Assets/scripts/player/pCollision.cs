using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pCollision : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8 || collision.gameObject.layer == 16 || collision.gameObject.layer == 17)
        {
            pController.jumpTriggerActive = true;
        }
        if (collision.tag == "climbable")
        {
            pController.climbable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 16 || collision.gameObject.layer == 17)
        {
            StartCoroutine("jumpDeactive");
        }
        if (collision.tag == "climbable")
        {
            pController.climbable = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "rideMe" && collision.GetContact(0).normal.y >= 0.9f)
        {
            pController.playerTransform.parent = collision.transform;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 16 || collision.gameObject.layer == 17)
        {
            pController.collisionActive = true;
            pController.touchedCollision = collision.GetContact(0).normal;
            foreach(ContactPoint2D contact in collision.contacts)
            {
                if(contact.normal.y > 0.4f)
                {
                    pController.touchedCollision2 = contact.normal;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 16 || collision.gameObject.layer == 17)
        {
            StartCoroutine("collisionDeactive");
            pController.touchedCollision = Vector2.zero;
            pController.touchedCollision2 = Vector2.zero;
        }
        if (collision.gameObject.tag == "rideMe")
        {
            pController.playerTransform.parent = null;
        }
    }
    IEnumerator jumpDeactive()
    {
        yield return new WaitForSeconds(0.1f);
        pController.jumpTriggerActive = false;
    }

    IEnumerator collisionDeactive()
    {
        yield return new WaitForSeconds(0.1f);
        pController.collisionActive = false;
    }
}
