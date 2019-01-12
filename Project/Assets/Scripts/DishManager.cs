using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    [Header("Dishes")]
    [SerializeField]
    Dish servingDish;

    [SerializeField]
    Dish waitingDish;

    [Header("Game Balance")]
    [SerializeField]
    float servingSpeed = 10f;

    [SerializeField, Range(0, 10)]
    float minAnticipationTime = 0.5f;

    [SerializeField, Range(0, 10)]
    float maxAnticipationTime = 4f;

    [SerializeField, Range(0, 10), Tooltip("The amount of time the players have to grab the food")]
    float reactionTime = 1f;

    float dishStart = 7f;
    float dishEnd = -7f;

    void OnValidate()
    {
        if (minAnticipationTime > maxAnticipationTime)
            minAnticipationTime = maxAnticipationTime;
    }

    void Start()
    {
        if (Debug.isDebugBuild)
            if (servingDish == null || waitingDish == null)
                Debug.LogError("There is no reference to one of the dishes");

        InitializeDishes(servingDish);
        InitializeDishes(waitingDish);

        StartDishes();
    }

    public void StartDishes()
    {
        servingDish.Serve();
    }

    void InitializeDishes(Dish dish)
    {
        dish.speed = -servingSpeed;
        dish.start = dishStart;
        dish.end = dishEnd;
        dish.servePosition = 0;
        dish.readyCallback = DishReady;
    }

    void Update()
    {

    }

    void LateUpdate()
    {
        if (!servingDish.foodIsGrabbable)
            return;

        bool leftGrab = GameController.instance.leftPaw.canGrabFood;
        bool rightGrab = GameController.instance.rightPaw.canGrabFood;

        if (leftGrab && !rightGrab)
        {
            servingDish.GrabFood();
            servingDish.Remove();
            ScoreManager.instance.AddScore(100, true);
        }
        else if (!leftGrab && rightGrab)
        {
            servingDish.GrabFood();
            servingDish.Remove();
            ScoreManager.instance.AddScore(100, false);
        }
        else if (leftGrab && rightGrab)
        {
            //Debug.Log("No one gets the food");
        }
    }

    void DishReady()
    {
        Invoke("ShowFood", Random.Range(minAnticipationTime, maxAnticipationTime));
    }

    void SwapDishes()
    {
        var swap = servingDish;
        servingDish = waitingDish;
        waitingDish = swap;
    }

    void ServeNext()
    {
        SwapDishes();

        servingDish.Serve();
        waitingDish.Remove();
    }

    void ShowFood()
    {
        servingDish.ShowFood();
        Invoke("ServeNext", reactionTime);
    }
}
