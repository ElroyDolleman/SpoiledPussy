using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Cat Paws")]
    [SerializeField]
    Paw leftPaw = null;
    [SerializeField]
    Paw rightPaw = null;
    [SerializeField]
    float pawSpeed = 60;

    void Start()
    {
        if (Debug.isDebugBuild)
        {
            if (leftPaw == null)
                Debug.LogError("There is no reference to the left paw");
            if (rightPaw == null)
                Debug.LogError("There is no reference to the right paw");
        }

        leftPaw.speed = pawSpeed;
        rightPaw.speed = pawSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(leftPaw.keyToGrab))
            leftPaw.Grab();

        if (Input.GetKeyDown(rightPaw.keyToGrab))
            rightPaw.Grab();
    }
}
