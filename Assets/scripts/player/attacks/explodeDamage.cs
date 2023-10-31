using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodeDamage : MonoBehaviour
{
    private ParticleSystem ps;
    private float explodeTimer = 0.1f;
    public CircleCollider2D colliderTrig;
    private int hitID;
    public int knockback;
    public Material matColor;
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        hitID = Random.Range(int.MinValue, int.MaxValue);
        if (pController.SUPERLOB)
        {
            colliderTrig.radius = 1.5f;
            ps.startSize = 1f;
            matColor.color = new Color(1.0f, 0.64f, 0f, 0.3f);
        }
        else
        {
            matColor.color = new Color(0f, 1f, 1f, 0.2f);
        }
    }
    private void Update()
    {
        if (!ps.isPlaying)
        {
            Destroy(gameObject);
        }
        explodeTimer -= Time.deltaTime;
        if(explodeTimer < 0f)
        {
            colliderTrig.enabled = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            if (!collision.isTrigger)
            {
                if (pController.SUPERLOB)
                {
                    collision.gameObject.SendMessage("TakeDamage", new int[] { 10, hitID, 2, knockback }, SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    collision.gameObject.SendMessage("TakeDamage", new int[] { 1, hitID, 2, knockback }, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
