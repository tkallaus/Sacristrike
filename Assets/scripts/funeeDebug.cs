using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class funeeDebug : MonoBehaviour
{
    private int fun = 0;
    void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            fun++;
            //transform.Rotate(fun, fun, fun);
            GetComponent<Camera>().cullingMask = ~0;
        }
    }
}
