using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField]
    Text leftScore = null;

    [SerializeField]
    Text rightScore = null;

    struct PlayerData
    {
        public bool isLeftPlayer;
        public int score;
        public int grabbedFoodAmount;
    }

    PlayerData leftPlayerData;
    PlayerData rightPlayerData;

    void Start()
    {
        if (Debug.isDebugBuild)
        {
            if (instance != null)
                Debug.LogError("There should only be one instance of the GameController");
            if (leftScore == null)
                Debug.LogError("There is no reference to a text field for the left player score");
            if (rightScore == null)
                Debug.LogError("There is no reference to a text field for the right player score");
        }

        instance = this;

        leftPlayerData.score = 0;
        leftPlayerData.grabbedFoodAmount = 0;
        leftPlayerData.isLeftPlayer = true;

        rightPlayerData.score = 0;
        rightPlayerData.grabbedFoodAmount = 0;
        rightPlayerData.isLeftPlayer = false;
    }

    public void AddScore(int amount, bool isLeftPlayer)
    {
        if (isLeftPlayer)
            AddScore(amount, ref leftPlayerData, leftScore);
        else
            AddScore(amount, ref rightPlayerData, rightScore);
    }

    void AddScore(int amount, ref PlayerData data, Text textfield)
    {
        data.score += amount;
        data.grabbedFoodAmount++;

        textfield.text = data.grabbedFoodAmount.ToString();
    }
}
