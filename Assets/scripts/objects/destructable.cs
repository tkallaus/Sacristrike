using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class destructable : MonoBehaviour
{
    public int HP;
    private Vector3 collisionPos;
    private Tilemap map;
    private void Start()
    {
        map = GetComponent<Tilemap>();
    }
    public void setPos(Vector3 pos)
    {
        collisionPos = pos;
    }
    public void destroyCheck(float[] inputs) //0 = damage, 1 = radius
    {
        if(inputs[0] >= HP)
        {
            Vector3 hitPos = Vector3.zero;
            for (int i = 0; i < 8; i++)
            {
                hitPos = collisionPos + (inputs[1] * 1.5f * (Quaternion.Euler(0, 0, 45 * i) * transform.right));
                map.SetTile(map.WorldToCell(hitPos), null);
            }
            for (int i = 0; i < 8; i++)
            {
                hitPos = collisionPos+(inputs[1] * (Quaternion.Euler(0, 0, 45 * i) * transform.right));
                map.SetTile(map.WorldToCell(hitPos), null);
            }
            for (int i = 0; i < 8; i++)
            {
                hitPos = collisionPos+((inputs[1]/2f) * (Quaternion.Euler(0, 0, 45 * i) * transform.right));
                map.SetTile(map.WorldToCell(hitPos), null);
            }
            map.SetTile(map.WorldToCell(collisionPos), null);
        }
    }
}
