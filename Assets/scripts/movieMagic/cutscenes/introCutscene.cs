using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class introCutscene : cutsceneTemplate
{
    //actors: 0 = kid, 1 = camCollider, 2 = flashscreen, 3 = camera, 4 = doorblock, 5 = podlight, 6-9 = beams1-4, 10 = screenlight, 11 = screenlightred, 12 = sweatdrops
    private Color flashColor;
    public GameObject player;
    public GameObject UI;
    private AudioSource[] sfx;
    private void Start()
    {
        flashColor = Color.white;
        sfx = actors[0].GetComponents<AudioSource>();
    }
    private void Update()
    {
        Time.timeScale = debugTimescale;
    }
    void FixedUpdate()
    {
        if(cutsceneTimer == 0)
        {
            actors[0].GetComponent<Rigidbody2D>().velocity = new Vector2(5, 3);
            actors[0].GetComponent<Animator>().Play("shocked");
            sfx[0].Stop();
            sfx[3].Play();
            sfx[2].Play();
            actors[1].transform.parent = null;
            actors[2].SetActive(true);
            actors[3].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.8f;
            actors[4].SetActive(false);
            actors[5].SetActive(true);
            actors[10].SetActive(false);
            actors[11].SetActive(true);
        }
        if (cutsceneTimer == 5)
        {
            actors[2].SetActive(false);
        }
        if (cutsceneTimer == 45)
        {
            actors[0].GetComponent<Animator>().Play("worried");
            actors[12].SetActive(true);
        }
        if (cutsceneTimer == 70)
        {
            actors[2].SetActive(true);
            sfx[1].Play();
            actors[6].SetActive(true);
            actors[3].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.9f;
        }
        if (cutsceneTimer == 75)
        {
            actors[2].SetActive(false);
        }
        if (cutsceneTimer == 140)
        {
            actors[2].SetActive(true);
            sfx[1].Play();
            actors[7].SetActive(true);
            actors[3].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1.0f;
        }
        if (cutsceneTimer == 145)
        {
            actors[2].SetActive(false);
        }
        if (cutsceneTimer == 175)
        {
            actors[0].GetComponent<titleController>().horizontalAxis = 3f;
        }
        if (cutsceneTimer == 210)
        {
            actors[2].SetActive(true);
            sfx[1].Play();
            actors[8].SetActive(true);
            actors[3].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1.1f;
        }
        if (cutsceneTimer == 215)
        {
            actors[2].SetActive(false);
        }
        if (cutsceneTimer == 245)
        {
            actors[2].SetActive(true);
            sfx[1].Play();
            actors[9].SetActive(true);
            actors[3].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1.2f;
        }
        if (cutsceneTimer > 250)
        {
            actors[2].GetComponent<SpriteRenderer>().color = Color.Lerp(actors[2].GetComponent<SpriteRenderer>().color, flashColor, 0.01f);
        }
        if (cutsceneTimer == 800)
        {
            sfx[3].Stop();
            player.SetActive(true);
            UI.SetActive(true);
            actors[3].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
            foreach (GameObject actor in actors)
            {
                actor.SetActive(false);
            }
            gameObject.SetActive(false);
        }
        cutsceneTimer++;
    }
}
