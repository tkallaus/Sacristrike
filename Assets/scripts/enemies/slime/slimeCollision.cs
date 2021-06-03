using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeCollision : MonoBehaviour
{
    public slime parent;
    private int[] hitIDs; //One for each possible source of damage; 0 = slash, 1 = lob, 2 = lobExplosion

    private void Start()
    {
        hitIDs = new int[3];
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        parent.collisionActive = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        parent.collisionActive = false;
    }

    private void TakeDamage(int[] inputs) //0 = damageAmount, 1 = hitID, 2 = damageSource, 3 = knockback
    {
        if(inputs[1] != hitIDs[inputs[2]])
        {
            parent.HP -= inputs[0];
            hitIDs[inputs[2]] = inputs[1];
            parent.damageInterrupt(inputs[3]);
        }
    }
}
