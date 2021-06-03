using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energyBarUpdate : MonoBehaviour
{
    void Update()
    {
        transform.localScale = new Vector3(pController.energy / pController.energyMax, 1, 1);
    }
}
