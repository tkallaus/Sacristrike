using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossLogic : MonoBehaviour
{
    //TerrainMovement//
    public Transform terrainObject;
    private Transform[] terrainArray;
    private Transform[] patternArray;
    private Vector2 patternZeroPosition;

    private float[] speed;
    private float[] magnitude;
    private float[] differenceFromZero;

    private int patternActive = -1;
    private bool patternSet = false;
    public int patternTimer;

    //Boss//
    public int bossTimer;
    private int bossState;
    public static int HP = 20;
    public Vector2[] leapPoints;
    private Rigidbody2D rig;
    public GameObject bossObj;
    private Animator bossAnimator;
    private SpriteRenderer bossSprite;
    private bool busy = false;
    private float distanceToLeapPoint;
    private int selectedLeapPoint;
    public GameObject bossWeapon;
    public Transform playerRef;
    public AnimationCurve leapCurve;
    private int shootTimer = 0;
    public Transform bulletspawn;
    public Rigidbody2D bullet;
    private int hangShootCount = 0;
    public static bool touchingGround = false;

    //Credits or somethin//
    public UnityEngine.UI.Image congratz;
    public UnityEngine.UI.Image pressESC;
    private bool dedForASec = false;
    public UnityEngine.UI.Text finalTime;
    private bool timeAdded = false;

    //Test/Debug//
    public float debugtimescale = 1;
    //Timer ref: At a speed of 1, it takes 314 iterations of patternTimer to touch the ground again
    void Start()
    {
        //TerrainMovement//
        terrainArray = new Transform[32];
        patternArray = new Transform[4];
        speed = new float[4];
        magnitude = new float[4];
        differenceFromZero = new float[4];

        int i = 0;
        foreach(Transform obj in terrainObject)
        {
            if (obj.CompareTag("Untagged"))
            {
                terrainArray[i] = obj;
                i++;
            }
        }
        i = 0;
        foreach (Transform obj in terrainObject)
        {
            if (obj.CompareTag("rideMe"))
            {
                patternArray[i] = obj;
                i++;
            }
        }
        patternZeroPosition = patternArray[0].position;

        //Boss//
        bossTimer = 0;
        bossState = 0;
        rig = GetComponent<Rigidbody2D>();
        bossAnimator = bossObj.GetComponent<Animator>();
        bossSprite = bossObj.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        //Test/Debug//
        Time.timeScale = debugtimescale;

        //TerrainMovement//
        if(patternActive == -1)
        {
            patternActive = Random.Range(0, 1);
        }
        else
        {
            switch (patternActive)
            {
                case 0:
                    pattern0();
                    break;
                case -2:
                    //patternSwapDelay
                    break;
                default:
                    Debug.Log("pattern broke");
                    break;
            }
        }
        foreach (Transform tform in patternArray)
        {
            if (tform.localPosition.y < 0)
            {
                tform.localPosition = new Vector2(0, 0);
            }
        }

        //Boss//
        if(bossTimer % 150 == 0 && HP > 0)
        {
            leap();
        }
        if (!busy)
        {
            switch (bossState)
            {
                case 0:
                    break;
                case 1:
                    hangAndShoot();
                    break;
                default:
                    break;
            }
            bossTimer++;
        }
        if (transform.position.x > playerRef.position.x)
        {
            bossSprite.flipX = false;
            //hardcoded cause plz let my suffering end
            bossWeapon.transform.localPosition = new Vector2(-0.46875f, 0.125f);
        }
        else
        {
            bossSprite.flipX = true;
            bossWeapon.transform.localPosition = new Vector2(0.46875f, 0.125f);
        }
        if(HP <= 0 && !dedForASec)
        {
            bossState = 3;
            patternActive = -2;
            bossWeapon.SetActive(false);
            rig.isKinematic = false;
            
            if (touchingGround)
            {
                bossAnimator.Play("ded");
                StartCoroutine("credsStart");
            }
            else
            {
                //weird thing to make sure the ded animation above is actually on the ground, it's awful but it won't be running for long.
                CapsuleCollider2D cap = bossSprite.gameObject.GetComponent<CapsuleCollider2D>();
                cap.direction = CapsuleDirection2D.Horizontal;
                cap.size = new Vector2(1f, 0.42f);

                bossAnimator.Play("falling");
            }
        }
        if (dedForASec)
        {
            congratz.gameObject.SetActive(true);
            pressESC.gameObject.SetActive(true);
            pController.playerTransform.gameObject.SendMessage("cutsceneSetter", true, SendMessageOptions.DontRequireReceiver);

            finalTime.gameObject.SetActive(true);
            if (!timeAdded)
            {
                finalTime.GetComponent<UnityEngine.UI.Text>().text += pController.speedrunTimer.Elapsed.ToString("hh\\:mm\\:ss\\.ff");
                timeAdded = true;
            }
        }
        Debug.Log(HP);
        if (pController.pcDead)
        {
            transform.position = new Vector2(0, 200);
            rig.isKinematic = false;
            HP = 20;
            bossState = 0;
            bossTimer = 0;
            shootTimer = 0;
            patternTimer = 0;
            bossWeapon.SetActive(false);
            bossSprite.flipX = false;
            rig.simulated = false;
            for(int i = 0; i < 4; i++)
            {
                patternArray[i].position = patternZeroPosition;
            }
            enabled = false;
        }
    }

    //Boss//
    private void leap()
    {
        if (!busy)
        {
            if(bossState == 0)
            {
                rig.isKinematic = true;
                distanceToLeapPoint = Vector2.Distance(transform.position, leapPoints[0]);
            }
            else
            {
                bossWeapon.SetActive(false);
                selectedLeapPoint++;
                if(selectedLeapPoint >= leapPoints.Length)
                {
                    selectedLeapPoint = 0;
                }
                distanceToLeapPoint = Vector2.Distance(transform.position, leapPoints[selectedLeapPoint]);
            }
        }
        if(bossState != 1)
        {
            busy = true;
            transform.position = Vector2.MoveTowards(transform.position, leapPoints[selectedLeapPoint], distanceToLeapPoint / 100f);
            bossAnimator.Play("jumping");
            bossSprite.flipX = true;
            float distPercent = Vector2.Distance(transform.position, leapPoints[selectedLeapPoint]) / distanceToLeapPoint;

            bossObj.transform.localPosition = new Vector2(0, leapCurve.Evaluate(distPercent*2)) * 5;

            if (Vector2.Distance(transform.position, leapPoints[selectedLeapPoint]) == 0f)
            {
                bossObj.transform.localPosition = Vector2.zero;
                busy = false;
                bossState = 1;
                bossSprite.flipX = false;
                //take out later?
                bossAnimator.Play("hanging");
            }
        }
    }

    private void hangAndShoot()
    {
        //bossAnimator.Play("hanging");
        bossWeapon.SetActive(true);
        bossWeapon.transform.right = bossWeapon.transform.position - playerRef.position;
        if(shootTimer % 150 == 0)
        {
            if (hangShootCount >= 4)
            {
                hangShootCount = 0;
                bossState = 2;
                shootTimer = 0;
            }
            else
            {
                StartCoroutine("shotPattern");
            }
        }
        shootTimer++;
    }

    //TerrainMovement//
    private void pattern0()
    {
        if (!patternSet)
        {
            terrainArray[4].transform.parent = patternArray[0];
            terrainArray[5].transform.parent = patternArray[0];
            terrainArray[6].transform.parent = patternArray[0];
            terrainArray[7].transform.parent = patternArray[0];
            terrainArray[10].transform.parent = patternArray[1];
            terrainArray[11].transform.parent = patternArray[1];
            terrainArray[12].transform.parent = patternArray[1];
            terrainArray[13].transform.parent = patternArray[1];
            terrainArray[18].transform.parent = patternArray[2];
            terrainArray[19].transform.parent = patternArray[2];
            terrainArray[20].transform.parent = patternArray[2];
            terrainArray[21].transform.parent = patternArray[2];
            terrainArray[24].transform.parent = patternArray[3];
            terrainArray[25].transform.parent = patternArray[3];
            terrainArray[26].transform.parent = patternArray[3];
            terrainArray[27].transform.parent = patternArray[3];
            for (int i = 0; i < speed.Length; i++)
            {
                if (i % 2 == 0)
                {
                    speed[i] = 2f;
                }
                else
                {
                    speed[i] = 1f;
                }
            }
            for (int i = 0; i < magnitude.Length; i++)
            {
                if (i % 2 == 0)
                {
                    magnitude[i] = 3f;
                }
                else
                {
                    magnitude[i] = 6f;
                }
            }
            patternTimer = 0;
            patternSet = true;
        }
        if (patternTimer == 0)
        {
            differenceFromZero[0] = 0 - Time.time;
        }
        if (patternTimer >= 0 && patternTimer <= 160)
        {
            patternArray[0].transform.position = patternZeroPosition + new Vector2(0, Mathf.Sin((Time.time + differenceFromZero[0]) * speed[0]) * magnitude[0]);
        }
        if (patternTimer == 40)
        {
            differenceFromZero[1] = 0 - Time.time;
        }
        if (patternTimer >= 40 && patternTimer <= 360)
        {
            patternArray[1].transform.position = patternZeroPosition + new Vector2(0, Mathf.Sin((Time.time + differenceFromZero[1]) * speed[1]) * magnitude[1]);
        }
        if (patternTimer == 80)
        {
            differenceFromZero[2] = 0 - Time.time;
        }
        if (patternTimer >= 80 && patternTimer <= 240)
        {
            patternArray[2].transform.position = patternZeroPosition + new Vector2(0, Mathf.Sin((Time.time + differenceFromZero[2]) * speed[2]) * magnitude[2]);
        }
        if (patternTimer == 120)
        {
            differenceFromZero[3] = 0 - Time.time;
        }
        if (patternTimer >= 120 && patternTimer <= 440)
        {
            patternArray[3].transform.position = patternZeroPosition + new Vector2(0, Mathf.Sin((Time.time + differenceFromZero[3]) * speed[3]) * magnitude[3]);
        }
        patternTimer++;

        if (patternTimer == 450)
        {
            foreach(Transform ter in terrainArray)
            {
                ter.parent = terrainObject;
            }
            patternActive = -2;
            patternSet = false;
            StartCoroutine("patternDelay");
        }
    }

    IEnumerator patternDelay()
    {
        yield return new WaitForSeconds(1f);
        patternActive = -1;
    }
    IEnumerator shotPattern()
    {
        hangShootCount++;
        Rigidbody2D temp = Instantiate(bullet, bulletspawn.position, bossWeapon.transform.rotation);
        temp.velocity = (playerRef.position - bulletspawn.position).normalized * 10;
        yield return new WaitForSeconds(0.1f);
        temp = Instantiate(bullet, bulletspawn.position, bossWeapon.transform.rotation);
        temp.velocity = (playerRef.position - bulletspawn.position).normalized * 10;
        yield return new WaitForSeconds(0.1f);
        temp = Instantiate(bullet, bulletspawn.position, bossWeapon.transform.rotation);
        temp.velocity = (playerRef.position - bulletspawn.position).normalized * 10;
    }
    IEnumerator credsStart()
    {
        yield return new WaitForSeconds(3f);
        dedForASec = true;
    }
}
