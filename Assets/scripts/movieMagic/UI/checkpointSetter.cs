using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointSetter : MonoBehaviour
{
    public GameObject checkpointImg;

    private void OnEnable()
    {
        pController.playerSpawn = transform.position;
        checkpointImg.SetActive(true);
        StartCoroutine("checkpointDisable");
    }
    private void OnDisable()
    {
        if(checkpointImg != null)
        {
            checkpointImg.SetActive(false);
        }
    }

    IEnumerator checkpointDisable()
    {
        yield return new WaitForSeconds(2f);
        checkpointImg.SetActive(false);
    }
}
