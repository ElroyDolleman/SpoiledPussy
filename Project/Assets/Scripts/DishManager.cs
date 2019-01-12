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

    void DishReady()
    {
        Invoke("ServeNext", Random.Range(minAnticipationTime, maxAnticipationTime));
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
}
