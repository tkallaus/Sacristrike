using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossIntroScene : cutsceneTemplate
{
    //Actor Ref: 0 = Player, 1 = Boss, 2 = BossSprite/Colliders, 3 = RockParticleSystem, 4 = 1st wave terrain, 5-6 = 2nd wave terrain, 7-8 = 3rd wave terrain
    private float[] differenceFromZero;
    private Vector2[] positions; 
    void Start()
    {
        positions = new Vector2[5];
        positions[0] = actors[4].transform.position;
        positions[1] = actors[5].transform.position;
        positions[2] = actors[6].transform.position;
        positions[3] = actors[7].transform.position;
        positions[4] = actors[8].transform.position;
        differenceFromZero = new float[3];
    }

    void FixedUpdate()
    {
        Time.timeScale = debugTimescale;
        if(cutsceneTimer == 0)
        {
            actors[0].GetComponent<pController>().horizontalAxis = -1f;
            actors[0].GetComponent<pController>().jumping = true;
            actors[1].transform.localPosition = new Vector2(0f, -5.71f);
            actors[3].GetComponent<ParticleSystem>().Play();
        }
        if(cutsceneTimer == 64)
        {
            actors[0].GetComponent<pController>().horizontalAxis = 1f;
        }
        if(cutsceneTimer == 65)
        {
            actors[0].GetComponent<pController>().horizontalAxis = 0f;
        }
        if(cutsceneTimer == 120)
        {
            actors[1].transform.localPosition = new Vector2(0f, -4.52f);
            actors[1].GetComponent<Rigidbody2D>().simulated = true;
            actors[1].GetComponent<Rigidbody2D>().velocity = new Vector2(4f, 20f);
            actors[2].GetComponent<Animator>().Play("jumping");

            differenceFromZero[0] = 0f - Time.time;
        }
        if(cutsceneTimer >= 120 && cutsceneTimer <= 434)
        {
            actors[4].transform.position = positions[0] + new Vector2(0, Mathf.Sin(Time.time + differenceFromZero[0]) * 8f) * 100 * Time.fixedDeltaTime;
        }
        if (cutsceneTimer == 160)
        {
            differenceFromZero[1] = 0f - Time.time;
        }
        if (cutsceneTimer >= 160 && cutsceneTimer <= 474)
        {
            actors[5].transform.position = positions[1] + new Vector2(0, Mathf.Sin(Time.time + differenceFromZero[1]) * 8f) * 100 * Time.fixedDeltaTime;
            actors[6].transform.position = positions[2] + new Vector2(0, Mathf.Sin(Time.time + differenceFromZero[1]) * 8f) * 100 * Time.fixedDeltaTime;
        }
        if (cutsceneTimer == 200)
        {
            differenceFromZero[2] = 0f - Time.time;
        }
        if (cutsceneTimer >= 200 && cutsceneTimer <= 514)
        {
            actors[7].transform.position = positions[3] + new Vector2(0, Mathf.Sin(Time.time + differenceFromZero[2]) * 8f) * 100 * Time.fixedDeltaTime;
            actors[8].transform.position = positions[4] + new Vector2(0, Mathf.Sin(Time.time + differenceFromZero[2]) * 8f) * 100 * Time.fixedDeltaTime;
        }
        if (cutsceneTimer == 201)
        {
            actors[2].GetComponent<Animator>().Play("standing");
        }
        if (cutsceneTimer == 515)
        {
            actors[4].transform.position = positions[0];
            actors[5].transform.position = positions[1];
            actors[6].transform.position = positions[2];
            actors[7].transform.position = positions[3];
            actors[8].transform.position = positions[4];

            actors[1].GetComponent<bossLogic>().enabled = true;

            gameObject.SetActive(false);
        }
        cutsceneTimer++;
    }
}
