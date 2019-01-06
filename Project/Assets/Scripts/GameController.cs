using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    Paw leftPaw;

    [SerializeField]
    Paw rightPaw;

    void Start()
    {
        if (Debug.isDebugBuild)
        {
            if (leftPaw == null)
                Debug.LogError("There is no reference to the left paw");
            if (rightPaw == null)
                Debug.LogError("There is no reference to the right paw");
        }


    }

    void Update()
    {
        if (Input.GetKeyDown(leftPaw.keyToGrab))
            leftPaw.Grab();

        if (Input.GetKeyDown(rightPaw.keyToGrab))
            rightPaw.Grab();
    }
}
