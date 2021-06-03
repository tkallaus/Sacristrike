using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OptionEnum
{
    OneWay,
    BackAndForth,
    Looping
}

public class movingPlatforms : MonoBehaviour
{
    public OptionEnum MovementBehavior;
    public float speed;
    public Vector2[] positions;
    public Transform[] platforms;
    private movingPlatformsReceiver[] platformJourneyPositions;
    private int selected;
    private bool backAndForthSwitch = true;

    private Vector2[] startingPositions;
    private int[] startingJourneyPositions;

    public bool ActiveOnTouch;
    private bool rememberBool;

    void Start()
    {
        selected = (int)MovementBehavior;
        startingPositions = new Vector2[platforms.Length];
        startingJourneyPositions = new int[platforms.Length];

        platformJourneyPositions = new movingPlatformsReceiver[platforms.Length];
        for(int i = 0; i < platforms.Length; i++)
        {
            platformJourneyPositions[i] = platforms[i].GetComponent<movingPlatformsReceiver>();
            startingPositions[i] = platforms[i].position;
            startingJourneyPositions[i] = platformJourneyPositions[i].journeyPosition;
        }
    }

    private void FixedUpdate()
    {
        foreach(movingPlatformsReceiver platform in platformJourneyPositions)
        {
            if (platform.pCollided)
            {
                ActiveOnTouch = false;
            }
        }
        if (!ActiveOnTouch)
        {
            switch (selected)
            {
                case 0:
                    if (platformJourneyPositions[0].journeyPosition + 1 < positions.Length)
                    {
                        platforms[0].position = Vector2.MoveTowards(platforms[0].position, positions[platformJourneyPositions[0].journeyPosition + 1], speed * Time.fixedDeltaTime);
                        if (((Vector2)platforms[0].position - positions[platformJourneyPositions[0].journeyPosition + 1]).sqrMagnitude < 0.01f)
                        {
                            platformJourneyPositions[0].journeyPosition++;
                        }
                    }
                    break;
                case 1:
                    if (backAndForthSwitch)
                    {
                        if (platformJourneyPositions[0].journeyPosition + 1 < positions.Length)
                        {
                            platforms[0].position = Vector2.MoveTowards(platforms[0].position, positions[platformJourneyPositions[0].journeyPosition + 1], speed * Time.fixedDeltaTime);
                            if (((Vector2)platforms[0].position - positions[platformJourneyPositions[0].journeyPosition + 1]).sqrMagnitude < 0.01f)
                            {
                                platformJourneyPositions[0].journeyPosition++;
                            }
                        }
                        else
                        {
                            backAndForthSwitch = false;
                        }
                    }
                    else
                    {
                        if (platformJourneyPositions[0].journeyPosition - 1 >= 0)
                        {
                            platforms[0].position = Vector2.MoveTowards(platforms[0].position, positions[platformJourneyPositions[0].journeyPosition - 1], speed * Time.fixedDeltaTime);
                            if (((Vector2)platforms[0].position - positions[platformJourneyPositions[0].journeyPosition - 1]).sqrMagnitude < 0.01f)
                            {
                                platformJourneyPositions[0].journeyPosition--;
                            }
                        }
                        else
                        {
                            backAndForthSwitch = true;
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < platforms.Length; i++)
                    {
                        if (platformJourneyPositions[i].journeyPosition + 1 < positions.Length)
                        {
                            platforms[i].position = Vector2.MoveTowards(platforms[i].position, positions[platformJourneyPositions[i].journeyPosition + 1], speed * Time.fixedDeltaTime);
                            if (((Vector2)platforms[i].position - positions[platformJourneyPositions[i].journeyPosition + 1]).sqrMagnitude < 0.01f)
                            {
                                platformJourneyPositions[i].journeyPosition++;
                            }
                        }
                        else
                        {
                            platforms[i].position = positions[0];
                            platformJourneyPositions[i].journeyPosition = 0;
                        }
                    }
                    break;
            }
        }
    }
    private void OnEnable()
    {
        rememberBool = ActiveOnTouch;
    }
    private void OnDisable()
    {
        ActiveOnTouch = rememberBool;
        for(int i = 0; i < platforms.Length; i++)
        {
            platforms[i].position = startingPositions[i];
            platformJourneyPositions[i].journeyPosition = startingJourneyPositions[i];
        }
    }
}
