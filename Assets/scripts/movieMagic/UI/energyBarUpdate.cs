using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energyBarUpdate : MonoBehaviour
{
    public UnityEngine.UI.Image bar;
    private Color weirdCyan;
    private Color weirdOrange;
    private void Start()
    {
        weirdCyan = bar.color;
        weirdOrange = new Color(1f, 0.64f, 0f);
    }
    void Update()
    {
        transform.localScale = new Vector3(pController.energy / pController.energyMax, 1, 1);
        if (pController.energyOK)
        {
            bar.color = weirdCyan;
        }
        else
        {
            bar.color = weirdOrange;
        }
    }
}
