using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSFXTrigger : MonoBehaviour
{
    private AudioSource chirp;
    private float chirpCooldown = 2f;
    private void Start()
    {
        chirp = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (chirpCooldown < 0f)
        {
            chirpCooldown = Random.Range(1f, 5f);
            chirp.Play();
        }
        chirpCooldown -= Time.fixedDeltaTime;
    }
}
