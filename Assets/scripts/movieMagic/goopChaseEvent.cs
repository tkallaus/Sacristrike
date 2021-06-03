using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goopChaseEvent : MonoBehaviour
{
    public Transform goopChaseObj;
    private bool startRising;
    public float riseSpd;
    private void OnEnable()
    {
        goopChaseObj.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        startRising = false;
        if(goopChaseObj != null)
        {
            goopChaseObj.position = new Vector3(0, -1, 0);
            goopChaseObj.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            startRising = true;
        }
    }
    private void FixedUpdate()
    {
        if (startRising && goopChaseObj.position.y < 25)
        {
            goopChaseObj.Translate(0, riseSpd * Time.fixedDeltaTime, 0);
        }
    }
}
