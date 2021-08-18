using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossHit : MonoBehaviour
{
    private int[] hitIDs; //One for each possible source of damage; 0 = slash, 1 = lob, 2 = lobExplosion
    private SpriteRenderer spriteflash;
    private float flashtimer = -1f;

    private void Start()
    {
        hitIDs = new int[3];
        spriteflash = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(flashtimer >= 0f)
        {
            spriteflash.color = Color.red;
            flashtimer -= Time.deltaTime;
        }
        else
        {
            spriteflash.color = Color.white;
        }
    }
    private void TakeDamage(int[] inputs) //0 = damageAmount, 1 = hitID, 2 = damageSource, 3 = knockback
    {
        if (inputs[1] != hitIDs[inputs[2]])
        {
            bossLogic.HP -= inputs[0];
            hitIDs[inputs[2]] = inputs[1];
            flashtimer = 0.1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.GetContact(0).normal.y > 0.8)
        {
            bossLogic.touchingGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        bossLogic.touchingGround = false;
    }
}
