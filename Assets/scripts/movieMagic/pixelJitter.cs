using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pixelJitter : MonoBehaviour
{
    public float speed = 1f;
    private float timer;
    private bool negative = false;
    private void Start()
    {
        timer = 1f / speed;
    }
    private void FixedUpdate()
    {
        if(timer <= 0)
        {
            if (negative)
            {
                transform.Translate(0f, -0.0625f, 0f);
            }
            else
            {
                transform.Translate(0f, 0.0625f, 0f);
            }
            negative = !negative;
            timer = 1f / speed;
        }
        timer -= Time.fixedDeltaTime;
    }
}
