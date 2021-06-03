using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conveyors : MonoBehaviour
{
    public int conveyorVelocity;
    private List<Rigidbody2D> rigs;
    private void Start()
    {
        rigs = new List<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.rigidbody != null)
        {
            if(collision.GetContact(0).normal.y <= 0.1f)
            {
                rigs.Add(collision.rigidbody);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        rigs.Remove(collision.rigidbody);
    }
    private void FixedUpdate()
    {
        if(rigs.Count > 0)
        {
            foreach(Rigidbody2D rig in rigs)
            {
                if (rig.gameObject.layer == 9)
                {
                    pController.pFinalSpeed.x += conveyorVelocity;
                }
                else
                {
                    rig.velocity = new Vector2(conveyorVelocity, rig.velocity.y);
                }
            }
        }
    }
}
