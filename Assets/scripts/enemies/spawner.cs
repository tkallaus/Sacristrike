using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject toBeSpawned;
    public GameObject toBeKilled;

    private void OnEnable()
    {
        toBeKilled = Instantiate(toBeSpawned, transform.position, transform.rotation);
        toBeKilled.transform.parent = transform;
    }
    private void OnDisable()
    {
        Destroy(toBeKilled);
    }
}
