using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadein : MonoBehaviour
{
    private SpriteRenderer sprite;
    public bool active = false;
    public float delay;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (active && delay <= 0)
        {
            Color t = sprite.color;
            t.a += 0.01f;
            sprite.color = t;
            if(sprite.color.a >= 1)
            {
                active = false;
            }
        }
        else if (active && delay > 0)
        {
            delay -= Time.deltaTime;
        }
    }
}
