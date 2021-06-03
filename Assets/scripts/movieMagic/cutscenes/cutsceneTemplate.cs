using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class cutsceneTemplate : MonoBehaviour
{
    public GameObject[] actors;
    public int cutsceneTimer = 0;
    public float debugTimescale = 1;

    private void OnEnable()
    {
        foreach (GameObject actor in actors)
        {
            if (actor.tag == "controllable")
            {
                actor.SendMessage("cutsceneSetter", true, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    private void OnDisable()
    {
        foreach (GameObject actor in actors)
        {
            if (actor != null)
            {
                if (actor.tag == "controllable")
                {
                    actor.SendMessage("cutsceneSetter", false, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
