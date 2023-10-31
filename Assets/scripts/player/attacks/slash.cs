using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slash : MonoBehaviour
{
    public float reflectionCost;
    public int knockback;
    public int hitID;
    AudioSource reflectSFX;
    private void Start()
    {
        reflectSFX = GetComponent<AudioSource>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            if (!collision.isTrigger)
            {
                collision.gameObject.SendMessage("TakeDamage", new int[] { 1, hitID, 0, knockback }, SendMessageOptions.DontRequireReceiver);
            }
        }
        if(collision.gameObject.tag == "reflectable" && collision.gameObject.layer == 13)
        {
            Rigidbody2D velBody = collision.gameObject.GetComponent<Rigidbody2D>();
            velBody.velocity = -velBody.velocity * 3;
            collision.gameObject.layer = 10;
            pController.energy -= reflectionCost;
            reflectSFX.pitch = Random.Range(1.65f, 1.85f);
            reflectSFX.Play();
        }
    }
}
