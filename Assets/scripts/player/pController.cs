﻿using System.Collections;
using UnityEngine;

public class pController : MonoBehaviour
{

    private bool moving;
    public bool jumping;
    public float horizontalAxis;
    private float verticalAxis;
    public float movementSpeed;
    public float jumpHeight;
    private int jumpCount;
    public static int jumpMax;
    private bool jumpable;

    public static bool climbable = false;
    private bool climbing = false;

    public static bool jumpTriggerActive;
    public static bool collisionActive;

    private bool wallJumped;
    private float wallJumpedTimer = 0.1f;

    public static Vector2 touchedCollision;
    public static Vector2 touchedCollision2;
    public static string touchedTag;

    public GameObject player;
    private Rigidbody2D rig;
    public PhysicsMaterial2D frictionMat;

    public GameObject slashObject;
    private bool slashing;
    private float slashTimer = 0.1f;
    private Animator slashAnims;
    private SpriteRenderer slashFlip;
    private slash slashID;
    public float slashCost;

    public Transform lobSpawn;
    private bool lobbing;
    private float lobTimer = 0.5f;
    public float lobPower;
    public Rigidbody2D lob;
    private bool lobCharging;
    public float lobChargePower;
    private Animator lobChargeAnims;
    private SpriteRenderer lobChargeFlip;
    public float lobCost;
    
    private Animator animationController;
    private SpriteRenderer spriteFlip;
    private bool slashAnimSwitch = false;

    public float regenRate;
    public static float regenTimer;
    public static float energy;
    public static float energyMax;
    public static float pcHurtTimer;
    public static bool pcDead = false;
    public static bool takingDamage = false;
    public static float takingDamageTimer = 0.1f;

    public static bool energyOK = true;
    
    private LineRenderer lobTrajectory;

    public static Transform playerTransform;
    public static Vector2 playerSpawn;

    public static bool lobEnabled = false;

    public static Vector2 pFinalSpeed;

    private bool cutsceneControlled = false;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animationController = player.GetComponent<Animator>();
        spriteFlip = player.GetComponent<SpriteRenderer>();
        slashAnims = slashObject.GetComponent<Animator>();
        slashFlip = slashObject.GetComponent<SpriteRenderer>();
        lobChargeAnims = lobSpawn.GetComponent<Animator>();
        lobChargeFlip = lobSpawn.GetComponent<SpriteRenderer>();
        slashID = slashObject.GetComponent<slash>();
        energyMax = 100f;
        energy = energyMax;
        regenTimer = 3f;
        pcHurtTimer = 0f;
        lobTrajectory = lobSpawn.GetComponent<LineRenderer>();
        pFinalSpeed = Vector2.zero;

        playerTransform = transform;
        playerSpawn = transform.position;
    }
    
    void Update()
    {
        if (pcDead)
        {
            horizontalAxis = 0;
            verticalAxis = 0;
            moving = true;
            //animationController.SetBool("dead", pcDead); Make an animation where the character gets knocked back then crumples when they touch the ground.
        }
        else
        {
            if (!cutsceneControlled)
            {
                horizontalAxis = Input.GetAxis("Horizontal");
                verticalAxis = Input.GetAxis("Vertical");
            }

            if (horizontalAxis != 0)
            {
                moving = true;
            }
            else
            {
                moving = false;
            }
            animationController.SetBool("moving", moving);

            if (Input.GetButtonDown("Jump") && !cutsceneControlled)
            {
                jumping = true;
            }
            animationController.SetBool("jumping", jumping);

            if (Input.GetButtonDown("drop"))
            {
                Physics2D.IgnoreLayerCollision(9, 16, true);
            }
            if (Input.GetButtonUp("drop"))
            {
                StartCoroutine("dropRelease");
            }

            if (climbable)
            {
                if (verticalAxis != 0)
                {
                    climbing = true;
                }
                if (takingDamage)
                {
                    climbing = false;
                }
            }
            else
            {
                climbing = false;
            }

            if (Input.GetButtonDown("Fire1") && energyOK && !cutsceneControlled)
            {
                slashing = true;
            }

            if (Input.GetButton("Fire2") && lobTimer == 0.5f && energyOK && lobEnabled && !cutsceneControlled)
            {
                lobCharging = true;
                lobbing = true;
                if (Input.GetButton("Fire1"))
                {
                    lobCharging = false;
                    lobbing = false;
                    lobPower = 5f;
                    lobTimer = 0.5f;
                    lobSpawn.gameObject.SetActive(false);
                }
            }
            else
            {
                lobCharging = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (climbing)
        {
            pFinalSpeed += new Vector2(horizontalAxis*movementSpeed/2, verticalAxis*movementSpeed/2);
            rig.gravityScale = 0;
            jumpCount = 0;
        }
        else
        {
            rig.gravityScale = 5;
        }

        if (moving && !wallJumped && !takingDamage)
        {
            pFinalSpeed.x += horizontalAxis * movementSpeed;
        }

        if(jumpTriggerActive && collisionActive)
        {
            jumpCount = 0;
        }

        if(touchedCollision.y > 0.68f && touchedCollision.y < 0.72f)
        {
            frictionMat.friction = 1f;
            rig.sharedMaterial = frictionMat;
            rig.AddForce(-touchedCollision.normalized*100);
        }
        else
        {
            frictionMat.friction = 0f;
            rig.sharedMaterial = frictionMat;
        }

        //Rewrite at some point to be based on normals rather than triggers and collision AND normals
        if (jumping)
        {
            if (!jumpTriggerActive && collisionActive && touchedCollision.y < 0.1f && touchedCollision.y > -0.1f)
            {
                rig.velocity = new Vector2(touchedCollision.x * 10, jumpHeight / 1.5f);
                wallJumped = true;
                jumpCount = 0;
                jumping = false;
            }
            else if ((jumpTriggerActive && collisionActive && touchedCollision.y > 0.4f) || climbing)
            {
                pFinalSpeed.y += jumpHeight;
                jumping = false;
                climbing = false;
            }
            //stupid special case that fixes something that idk how I caused: not being able to jump if you jumped against a wall and slid to the ground
            else if ((jumpTriggerActive && collisionActive && (touchedCollision.y < 0.1f && touchedCollision.y > -0.1f && touchedCollision2.y > 0.4f)) || climbing)
            {
                pFinalSpeed.y += jumpHeight;
                jumping = false;
                climbing = false;
            }
            else if (jumpCount < jumpMax)
            {
                pFinalSpeed.y += jumpHeight / 2f;
                jumpCount++;
                jumping = false;
            }
            else
            {
                jumping = false;
            }
        }

        if (wallJumped && wallJumpedTimer > 0f)
        {
            wallJumpedTimer -= Time.fixedDeltaTime;
            wallJumped = true;
        }
        if(wallJumpedTimer <= 0f)
        {
            wallJumpedTimer = 0.1f;
            wallJumped = false;
        }

        if (slashing)
        {
            slashTimer -= Time.fixedDeltaTime;
            slashObject.SetActive(true);
            if(slashTimer < 0f)
            {
                slashObject.SetActive(false);
                slashing = false;
                slashTimer = 0.1f;
                slashID.hitID = Random.Range(int.MinValue, int.MaxValue);
                slashAnimSwitch = !slashAnimSwitch;
                pcTakeDamage(slashCost, true);
            }
            animationController.SetBool("slashSwitch", slashAnimSwitch);
            slashAnims.SetBool("slashSwitch", slashAnimSwitch);
            animationController.SetBool("slashing", slashing);
            slashAnims.SetBool("slashing", slashing);
        }

        if (lobCharging)
        {
            lobSpawn.gameObject.SetActive(true);
            lobPower += Time.fixedDeltaTime * lobChargePower;
            if(lobPower > 20f)
            {
                lobPower = 20f;
            }
            lobChargeAnims.SetFloat("lobPower", lobPower);

            SetTrajectoryPoints(lobSpawn.position, lobSpawn.up);
        }
        else if (lobbing)
        {
            if (lobTimer == 0.5f)
            {
                Rigidbody2D obj = Instantiate(lob, lobSpawn.position, transform.rotation);
                obj.velocity = lobSpawn.up * lobPower;
                pcTakeDamage(lobCost, true);
            }
            lobTimer -= Time.fixedDeltaTime;
            if (lobTimer < 0f)
            {
                lobbing = false;
                lobTimer = 0.5f;
            }
            lobPower = 5f;
            lobSpawn.gameObject.SetActive(false);
        }
        animationController.SetBool("lobCharging", lobCharging);

        if (!lobCharging && !slashing)
        {
            if (horizontalAxis > 0)
            {
                slashObject.transform.localPosition = new Vector2(1, -0.25f);
                lobSpawn.localPosition = new Vector2(0.674f, 0.393f);
                lobSpawn.up = new Vector2(1, 1);
                spriteFlip.flipX = false;
                slashFlip.flipX = false;
                lobChargeFlip.flipX = false;
            }
            else if (horizontalAxis < 0)
            {
                slashObject.transform.localPosition = new Vector2(-1, -0.25f);
                lobSpawn.localPosition = new Vector2(-0.674f, 0.393f);
                lobSpawn.up = new Vector2(-1, 1);
                spriteFlip.flipX = true;
                slashFlip.flipX = true;
                lobChargeFlip.flipX = true;
            }
        }
        animationController.SetFloat("verticalVelocity", rig.velocity.y);

        if (takingDamage && takingDamageTimer > 0f)
        {
            takingDamage = true;
            if (takingDamageTimer == 0.1f)
            {
                if (spriteFlip.flipX)
                {
                    rig.velocity = new Vector2(10, 10);
                }
                else
                {
                    rig.velocity = new Vector2(-10, 10);
                }
            }
            takingDamageTimer -= Time.deltaTime;
        }
        else
        {
            takingDamageTimer = 0.1f;
            takingDamage = false;
        }
        animationController.SetBool("isDead", pcDead);

        if (energy < energyMax && regenTimer < 0f && !pcDead && !lobCharging)
        {
            energy += regenRate * Time.fixedDeltaTime;
        }
        else if (energy >= energyMax)
        {
            energy = energyMax;
            energyOK = true;
        }
        else if(energy <= 0f)
        {
            energy = 0f;
            energyOK = false;
        }
        if(pcHurtTimer > 0f)
        {
            spriteFlip.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            spriteFlip.color = new Color(1, 1, 1, 1);
        }
        pcHurtTimer -= Time.fixedDeltaTime;
        regenTimer -= Time.fixedDeltaTime;

        if(!wallJumped && !takingDamage)
        {
            rig.velocity = new Vector2(pFinalSpeed.x, rig.velocity.y);
            pFinalSpeed.x = 0;
        }
        if (pFinalSpeed.y != 0)
        {
            rig.velocity = new Vector2(rig.velocity.x, pFinalSpeed.y) ;
            pFinalSpeed.y = 0;
        }
    }
    public static void pcTakeDamage(float amount, bool nonviolent = false)
    {
        if (!pcDead)
        {
            if (nonviolent)
            {
                energy -= amount;
                regenTimer = 3f;
            }
            else if (pcHurtTimer < 0f)
            {
                pcHurtTimer = 1f;
                if (energyOK)
                {
                    energy -= amount;
                    regenTimer = 3f;
                    takingDamage = true;
                }
                else
                {
                    pcDead = true;
                    takingDamage = true;
                }
            }
        }
    }

    void SetTrajectoryPoints(Vector3 posStart, Vector2 direction)
    {
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(direction.y, direction.x));

        float tTime = 0;

        tTime += 0.1f;

        lobTrajectory.SetPosition(0, posStart);

        for (int i = 1; i < lobTrajectory.positionCount; i++)
        {
            float dx = lobPower * tTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = lobPower * tTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (12.5f * tTime * tTime / 2.0f);
            Vector3 pos = new Vector3(posStart.x + dx/4, posStart.y + dy/4, 0);
            lobTrajectory.SetPosition(i, pos);
            tTime += 0.1f;
        }
    }

    IEnumerator dropRelease()
    {
        yield return new WaitForSeconds(0.1f);
        Physics2D.IgnoreLayerCollision(9, 16, false);
    }

    private void cutsceneSetter(bool active)
    {
        cutsceneControlled = active;
    }
}