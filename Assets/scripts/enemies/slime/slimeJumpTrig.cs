using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeJumpTrig : MonoBehaviour
{
    public slime parent;
    private void OnTriggerStay2D(Collider2D collision)
    {
        parent.jumpTrigActive = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        parent.jumpTrigActive = false;
    }
}
