using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderShoot : MonoBehaviour
{
    private Transform pTransform;
    public Rigidbody2D spiderShot;
    public float shotSpeed;
    public float reloadMaxTimer;
    private float reloadTimer;

    private int[] hitIDs; //One for each possible source of damage; 0 = slash, 1 = lob, 2 = lobExplosion
    private SpriteRenderer damageFlash;
    private float tookDamageFlashTimer = 0f;
    private Color redFlash;
    private Rigidbody2D rig;

    public int HP;

    void Start()
    {
        pTransform = FindObjectOfType<pController>().transform;
        reloadTimer = reloadMaxTimer;
        hitIDs = new int[3];
        damageFlash = GetComponent<SpriteRenderer>();
        redFlash = new Color(1, 0.5f, 0.5f);
        rig = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        reloadTimer -= Time.deltaTime;
        if(reloadTimer < 0f)
        {
            reloadTimer = reloadMaxTimer;
            Rigidbody2D temp = Instantiate(spiderShot, transform.position, transform.rotation);
            temp.velocity = (pTransform.position - transform.position).normalized * shotSpeed;
            temp.transform.parent = transform.parent.parent;
        }

        if (tookDamageFlashTimer > 0f)
        {
            damageFlash.color = redFlash;
        }
        else
        {
            damageFlash.color = Color.white;
        }
        tookDamageFlashTimer -= Time.deltaTime;

        if(HP <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    private void TakeDamage(int[] inputs) //0 = damageAmount, 1 = hitID, 2 = damageSource, 3 = knockback
    {
        if (inputs[1] != hitIDs[inputs[2]])
        {
            HP -= inputs[0];
            hitIDs[inputs[2]] = inputs[1];
            tookDamageFlashTimer = 0.1f;
            if((pTransform.position.x - transform.position.x) < 0f)
            {
                rig.velocity += new Vector2(2 + inputs[3], 0) * transform.right;
            }
            else
            {
                rig.velocity += new Vector2(2 + inputs[3], 0) * -transform.right;
            }
        }
    }
}
