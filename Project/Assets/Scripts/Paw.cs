using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paw : MonoBehaviour
{
    enum PawStates
    {
        Idle,
        Grabbing,
        PickFood,
        PullBack,
        Stun
    }

    public KeyCode keyToGrab;

    public float position { get => transform.position.x; set => transform.position = new Vector3(value, transform.position.y, transform.position.z); }

    public bool canGrabFood { get => currentState == PawStates.PickFood; }

    [NonSerialized]
    public float speed = 1f;

    [NonSerialized]
    public float foodPosition = 0;

    [NonSerialized]
    public float startPosition = 0;

    PawStates currentState = PawStates.Idle;

    void Start()
    {
        startPosition = transform.position.x;
    }

    public void CustomUpdate()
    {
        if (currentState == PawStates.PickFood)
            currentState = PawStates.PullBack;

        switch (currentState)
        {
            case PawStates.Grabbing:
                MoveTo(foodPosition, PawStates.PickFood);
                break;
            case PawStates.PullBack:
                MoveTo(startPosition, PawStates.Idle);
                break;
        }
    }

    void MoveTo(float destination, PawStates reachedDestinationState)
    {
        if (position != destination)
        {
            int dir = position < destination ? 1 : -1;
            position += speed * dir * Time.fixedDeltaTime;

            bool reachedDestination = dir == 1 ? position >= destination : position <= destination;

            if (reachedDestination)
            {
                position = destination;
                ChangeState(reachedDestinationState);
            }
        }
    }

    void ChangeState(PawStates newState)
    {
        currentState = newState;
    }

    public void Grab()
    {
        if (currentState == PawStates.Idle)
            ChangeState(PawStates.Grabbing);
    }
}
