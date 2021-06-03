using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodeDamage : MonoBehaviour
{
    private ParticleSystem ps;
    private float explodeTimer = 0.1f;
    public Collider2D colliderTrig;
    private int hitID;
    public int knockback;
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        hitID = Random.Range(int.MinValue, int.MaxValue);
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
                collision.gameObject.SendMessage("TakeDamage", new int[] { 1, hitID, 2, knockback }, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
