using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Some DistanceJoint and LineRenderer code borrowed from raywenderlich 2D Grappling Hook game tutorial

public class webSystem : MonoBehaviour
{
    public GameObject anchorPoint;
    public DistanceJoint2D webJoint;
    private bool webAttached;
    private Vector2 spiderPos;
    private Rigidbody2D webHingeAnchorRb;
    private SpriteRenderer webHingeAnchorSprite;

    public LineRenderer webRenderer;
    public LayerMask webLayerMask;
    private float webMaxCastDistance = 20f;
    private Vector2 webPosition;

    private bool distanceSet;

    private Vector3 aimDirection;

    private Rigidbody2D rig;
    //public float startingVelocity;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();

        webJoint.enabled = false;
        spiderPos = transform.position;
        webHingeAnchorRb = anchorPoint.GetComponent<Rigidbody2D>();
        webHingeAnchorSprite = anchorPoint.GetComponent<SpriteRenderer>();

        aimDirection = Quaternion.Euler(0, 0, 90f) * Vector2.right;

        StartCoroutine("dumbSimDelay");
    }

    void Update()
    {
        spiderPos = transform.position;
        
        if (!webAttached)
        {
            shootWeb(aimDirection);
        }
        else
        {
            UpdateWebPositions();
            transform.up = webPosition - spiderPos;
        }
    }
    
    private void shootWeb(Vector2 aimDirection)
    {
        webRenderer.enabled = true;

        var hit = Physics2D.Raycast(spiderPos, aimDirection, webMaxCastDistance, webLayerMask);
        
        if (hit.collider != null)
        {
            webAttached = true;
            if (webPosition != hit.point)
            {
                webPosition = hit.point;
                webJoint.distance = Vector2.Distance(spiderPos, hit.point);
                webJoint.enabled = true;
                webHingeAnchorSprite.enabled = true;
            }
            //rig.velocity = new Vector2(startingVelocity, 0);
        }
        else
        {
            webRenderer.enabled = false;
            webAttached = false;
            webJoint.enabled = false;
        }
    }

    private void UpdateWebPositions()
    {
        webRenderer.positionCount = 2;
        webRenderer.SetPosition(0, spiderPos);
        webRenderer.SetPosition(1, webPosition);
        webHingeAnchorRb.transform.position = webPosition;
        if (!distanceSet)
        {
            webJoint.distance = Vector2.Distance(spiderPos, webPosition);
            distanceSet = true;
        }
    }

    IEnumerator dumbSimDelay()
    {
        yield return new WaitForSeconds(0.5f);
        rig.simulated = true;
    }
}
