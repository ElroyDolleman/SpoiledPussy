using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("Cat Paws")]

    public Paw leftPaw = null;

    public Paw rightPaw = null;
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

            if (instance != null)
                Debug.LogError("There should only be one instance of the GameController");
        }

        instance = this;

        leftPaw.speed = pawSpeed;
        rightPaw.speed = pawSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(leftPaw.keyToGrab))
            leftPaw.Grab();

        if (Input.GetKeyDown(rightPaw.keyToGrab))
            rightPaw.Grab();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            leftPaw.Grab();
            rightPaw.Grab();
        }

        // Use a custom update to keep the execution order consistent
        leftPaw.CustomUpdate();
        rightPaw.CustomUpdate();
    }
}
