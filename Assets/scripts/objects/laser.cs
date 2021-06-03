using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    public LayerMask laserLayerMask;
    private LineRenderer laserRenderer;
    private CapsuleCollider2D lineCollider;

    public spawner[] spawners;
    private GameObject[] conditions;
    public bool lineActive = true;
    private bool anyAreAlive = true;

    public bool resetOnDisable;
    private void Start()
    {
        laserRenderer = GetComponent<LineRenderer>();
        lineCollider = gameObject.AddComponent<CapsuleCollider2D>();
        conditions = new GameObject[spawners.Length];
    }
    void Update()
    {
        if(spawners.Length > 0 && anyAreAlive)
        {
            spawnCheck();
        }
        if (lineActive)
        {
            lineCollider.enabled = true;
            laserRenderer.enabled = true;
            lineUpdate();
        }
        else
        {
            lineCollider.enabled = false;
            laserRenderer.enabled = false;
        }
    }
    private void lineUpdate()
    {
        var hit = Physics2D.Raycast(transform.position, transform.up, 50, laserLayerMask);
        if (hit.collider == null)
        {
            hit.point = transform.position+transform.up*50;
        }
        laserRenderer.positionCount = 2;
        laserRenderer.SetPosition(0, transform.position);
        laserRenderer.SetPosition(1, hit.point);
        Vector2 colliderSize = new Vector2(0.25f, (hit.point - (Vector2)transform.position).magnitude);
        lineCollider.offset = new Vector2(0,colliderSize.y/2);
        lineCollider.size = colliderSize;

        laserRenderer.widthMultiplier = Mathf.Sin(Time.fixedTime*5) * 0.25f + 0.75f;
    }
    private void spawnCheck()
    {
        int count = 0;
        foreach (spawner spawn in spawners)
        {
            conditions[count] = spawn.toBeKilled;
            count++;
        }
        anyAreAlive = false;
        foreach (GameObject condition in conditions)
        {
            if (condition != null)
            {
                anyAreAlive = true;
            }
        }
        lineActive = anyAreAlive;
    }

    private void OnDisable()
    {
        if (resetOnDisable)
        {
            lineActive = true;
        }
    }
}
