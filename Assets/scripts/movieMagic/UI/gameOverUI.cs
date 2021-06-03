using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOverUI : MonoBehaviour
{
    private RectTransform rectTransform;
    public RectTransform retryRect;
    private UnityEngine.UI.Image gameOverImg;
    private UnityEngine.UI.Image retryImg;
    private Color transparancy;
    private float timer1 = 2f;
    private float timer2 = 4f;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        gameOverImg = GetComponent<UnityEngine.UI.Image>();
        retryImg = retryRect.gameObject.GetComponent<UnityEngine.UI.Image>();
        transparancy = new Color(1, 1, 1, 0);
    }
    
    void Update()
    {
        if (pController.pcDead)
        {
            timer1 -= Time.deltaTime;
            timer2 -= Time.deltaTime;
            if(timer1 < 0f)
            {
                rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(0f, 100f), Time.deltaTime * 40);
                gameOverImg.color = Color.Lerp(gameOverImg.color, Color.white, Time.deltaTime);
            }
            if(timer2 < 0f)
            {
                retryRect.anchoredPosition = Vector2.MoveTowards(retryRect.anchoredPosition, new Vector2(0f, -100f), Time.deltaTime * 40);
                retryImg.color = Color.Lerp(retryImg.color, Color.white, Time.deltaTime);
                if (Input.GetButtonDown("Fire1"))
                {
                    pController.pcDead = false;
                    pController.playerTransform.position = new Vector2(-10000, -10000);
                    pController.energy = pController.energyMax;
                    StartCoroutine("frameDelay");
                }
            }
        }
        else
        {
            timer1 = 2f;
            timer2 = 4f;
            rectTransform.anchoredPosition = new Vector2(0f, 180f);
            retryRect.anchoredPosition = new Vector2(0f, -180f);
            gameOverImg.color = transparancy;
            retryImg.color = transparancy;
        }
    }

    IEnumerator frameDelay()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        pController.playerTransform.position = pController.playerSpawn;
    }
}
