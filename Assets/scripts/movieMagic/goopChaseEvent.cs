using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goopChaseEvent : MonoBehaviour
{
    public Transform goopChaseObj;
    private bool startRising;
    public float riseSpd;
    public Animator goopDrain;
    private AudioSource waterRisingSFX;
    public AudioClip alarmSFX;
    private float alarmTimer = 1f;

    private void Start()
    {
        waterRisingSFX = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        goopChaseObj.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        startRising = false;
        goopDrain.SetBool("runStart", false);
        if (goopChaseObj != null)
        {
            goopChaseObj.position = new Vector3(0, -1, 0);
            goopChaseObj.gameObject.SetActive(false);
        }
        waterRisingSFX.volume = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            startRising = true;
            goopDrain.SetBool("runStart", true);
        }
    }
    private void FixedUpdate()
    {
        if (startRising && goopChaseObj.position.y < 25)
        {
            goopChaseObj.Translate(0, riseSpd * Time.fixedDeltaTime, 0);
            if(alarmTimer <= 0)
            {
                waterRisingSFX.PlayOneShot(alarmSFX, 0.9f);
                alarmTimer = 1;
            }
            if(waterRisingSFX.volume < 0.3f)
            {
                waterRisingSFX.volume += 0.02f;
            }
            alarmTimer -= Time.fixedDeltaTime;
        }
        else
        {
            alarmTimer = 1;
        }
    }
}
