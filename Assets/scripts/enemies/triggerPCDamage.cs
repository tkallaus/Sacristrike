using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerPCDamage : MonoBehaviour
{
    public float damageAmount;
    public bool instakill = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (instakill)
            {
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
