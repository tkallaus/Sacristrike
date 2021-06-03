using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionPCDamage : MonoBehaviour
{
    public float damageAmount;
    public bool instakill = false;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (instakill)
            {
                //triggers knockback, even if damage is 0
                pController.pcTakeDamage(damageAmount);
                pController.pcDead = true;
                pController.energy = 0;
            }
            else
            {
                pController.pcTakeDamage(damageAmount);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (instakill)
            {
                pController.pcTakeDamage(damageAmount);
                pController.pcDead = true;
                pController.energy = 0;
            }
            else
            {
                pController.pcTakeDamage(damageAmount);
            }
        }
    }
}
