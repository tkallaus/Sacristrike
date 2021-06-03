using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleController : MonoBehaviour
{
    private bool moving;
    public float horizontalAxis;
    private Rigidbody2D rig;
    private Animator animationController;
    private SpriteRenderer spriteflip;

    public float movementSpeed;
    public float popFactor;
    private bool pop = false;

    private bool cutsceneControlled = false;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animationController = GetComponent<Animator>();
        spriteflip = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!cutsceneControlled)
        {
            horizontalAxis = Input.GetAxis("Horizontal");
        }

        if (horizontalAxis != 0)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        animationController.SetBool("isWalking", moving);

        if(horizontalAxis > 0)
        {
            spriteflip.flipX = true;
        }
        else if (horizontalAxis < 0)
        {
            spriteflip.flipX = false;
        }

    }

    private void FixedUpdate()
    {
        if (moving)
        {
            rig.velocity = new Vector2(horizontalAxis * movementSpeed, rig.velocity.y);
            if (pop)
            {
                rig.velocity = new Vector2(rig.velocity.x, popFactor);
                pop = false;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (collision.GetContact(0).normal.y > 0.9f)
            {
                pop = true;
            }
        }
    }

    private void cutsceneSetter(bool active)
    {
        cutsceneControlled = active;
    }
}
