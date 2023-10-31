using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime : MonoBehaviour
{
    private Rigidbody2D rig;
    private float actionTimer;
    private float moveTimer;
    private float cooldownTimer;
    private bool timeToAttack = false;
    private bool timeToMove = false;
    private bool moveRight;
    private Transform pTransform;
    private SpriteRenderer spriteFlip;
    private Animator animationController;
    private float tookDamageTimer = 0f;
    private float tookDamageFlashTimer = 0f;

    public bool collisionActive = false;
    public bool jumpTrigActive = false;
    private bool landed;
    private bool climbing = false;
    private readonly int worldMask = 1 << 8;

    private float playerXDistance;
    private float playerDistance;

    public int HP = 5;

    private Color redflash;

    private AudioSource squishFX;
    private bool sfxOneShot = true;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        actionTimer = 1f;
        moveTimer = 0.5f;
        cooldownTimer = 0.5f;
        pTransform = FindObjectOfType<pController>().transform;
        spriteFlip = GetComponent<SpriteRenderer>();
        animationController = GetComponent<Animator>();
        redflash = new Color(1, 0.5f, 0.5f);

        squishFX = GetComponent<AudioSource>();
    }

    private void Update()
    {
        playerXDistance = pTransform.position.x - transform.position.x;
        playerDistance = (pTransform.position - transform.position).sqrMagnitude;
        if (!timeToAttack && landed)
        {
            if (0f < playerXDistance)
            {
                moveRight = true;
            }
            else
            {
                moveRight = false;
            }
        }
        if(collisionActive && jumpTrigActive)
        {
            landed = true;
            if (sfxOneShot)
            {
                sfxOneShot = false;
                squishFX.pitch = Random.Range(0.9f, 1.1f);
                squishFX.Play();
            }
        }
        else
        {
            landed = false;
            sfxOneShot = true;
        }
        if (Mathf.Abs(playerDistance) < 9f && landed && cooldownTimer < 0f && tookDamageTimer < 0f)
        {
            timeToAttack = true;
            timeToMove = false;
        }
        else if (landed && tookDamageTimer < 0f && !timeToAttack)
        {
            timeToMove = true;
        }
        else
        {
            timeToMove = false;
        }
        if(HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (timeToAttack && !climbing)
        {
            actionTimer -= Time.fixedDeltaTime;
            if (actionTimer < 0f)
            {
                if (moveRight)
                {
                    Vector2 jumpV = new Vector2(10, 7);
                    rig.velocity = jumpV;
                    StartCoroutine(jumpExtraFrame(jumpV, rig));
                }
                else
                {
                    Vector2 jumpV = new Vector2(-10, 7);
                    rig.velocity = jumpV;
                    StartCoroutine(jumpExtraFrame(jumpV, rig));
                }
                actionTimer = 1f;
                timeToAttack = false;
                cooldownTimer = 0.5f;
                moveTimer = 0.5f;
            }
            animationController.SetFloat("actionTimer", actionTimer);
        }
        if(timeToMove && landed && !climbing)
        {
            moveTimer -= Time.fixedDeltaTime;
            if (moveTimer < 0f)
            {
                if (moveRight)
                {
                    if(Physics2D.Raycast(transform.position, transform.right, 1f, worldMask).collider != null)
                    {
                        climbing = true;
                    }
                    else
                    {
                        Vector2 jumpV = new Vector2(5, 7);
                        rig.velocity = jumpV;
                        StartCoroutine(jumpExtraFrame(jumpV, rig));
                    }
                }
                else
                {
                    if (Physics2D.Raycast(transform.position, -transform.right, 1f, worldMask).collider != null)
                    {
                        climbing = true;
                    }
                    else
                    {
                        Vector2 jumpV = new Vector2(-5, 7);
                        rig.velocity = jumpV;
                        StartCoroutine(jumpExtraFrame(jumpV, rig));
                    }
                }
                if (landed)
                {
                    moveTimer = 0.5f;
                }
            }
            animationController.SetFloat("moveTimer", moveTimer);
            if (moveRight)
            {
                spriteFlip.flipX = true;
            }
            else
            {
                spriteFlip.flipX = false;
            }
        }
        if (landed)
        {
            rig.velocity *= 0.8f;
        }
        if(tookDamageFlashTimer > 0f)
        {
            spriteFlip.color = redflash;
        }
        else
        {
            spriteFlip.color = Color.white;
        }
        animationController.SetBool("timeToAttack", timeToAttack);
        animationController.SetBool("timeToMove", timeToMove);
        animationController.SetBool("landed", landed);
        animationController.SetFloat("cooldownTimer", cooldownTimer);

        if (climbing)
        {
            if (moveRight)
            {
                rig.velocity = new Vector2(5, 7);
                StartCoroutine(slimeParkour());
            }
            else
            {
                rig.velocity = new Vector2(-5, 7);
                StartCoroutine(slimeParkour());
            }
        }

        cooldownTimer -= Time.fixedDeltaTime;
        tookDamageTimer -= Time.fixedDeltaTime;
        tookDamageFlashTimer -= Time.fixedDeltaTime;
    }

    public void damageInterrupt(float knockback)
    {
        if(knockback != 0)
        {
            if (0f < playerXDistance)
            {
                rig.velocity = new Vector2(-knockback, knockback);
            }
            else
            {
                rig.velocity = new Vector2(knockback, knockback);
            }
            actionTimer = 1f;
            moveTimer = 0.5f;
            timeToAttack = false;
            cooldownTimer = 0.5f;
            tookDamageTimer = 0.1f;
        }
        tookDamageFlashTimer = 0.1f;
    }

    public IEnumerator slimeParkour()
    {
        yield return new WaitForSeconds(0.2f);
        climbing = false;
    }

    public IEnumerator jumpExtraFrame(Vector2 jumpVec, Rigidbody2D rigToApply) //My hotfix to conveyorbelt movement, turns out it makes them more dangerous as well
    {
        yield return new WaitForSeconds(Time.fixedDeltaTime);
        rigToApply.velocity = jumpVec;
    }
}
