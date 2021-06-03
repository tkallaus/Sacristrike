using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animSelector : MonoBehaviour
{
    private Animator anims;
    public string animName;
    void Start()
    {
        anims = GetComponent<Animator>();
        anims.Play(animName);
    }
}
