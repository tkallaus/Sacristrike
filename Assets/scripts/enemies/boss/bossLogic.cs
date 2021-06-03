using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossLogic : MonoBehaviour
{
    public int HP = 100;
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

    public float debugtimescale = 1;
    //Timer ref: At a speed of 1, it takes 314 iterations of patternTimer to touch the ground again
    void Start()
    {
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
    }

    private void FixedUpdate()
    {
        Time.timeScale = debugtimescale;
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
    }
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
}
