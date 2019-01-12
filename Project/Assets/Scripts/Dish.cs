using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    public delegate void ReadyCallback();

    enum DishStates
    {
        Wait,
        Serve,
        Ready,
        ShowFood,
        Remove
    }

    DishStates currentState = DishStates.Wait;

    [SerializeField]
    GameObject cover = null;
    [SerializeField]
    GameObject food = null;

    [NonSerialized]
    public float easing;
    [NonSerialized]
    public float speed;
    [NonSerialized]
    public float start;
    [NonSerialized]
    public float end;
    [NonSerialized]
    public float servePosition;
    [NonSerialized]
    public ReadyCallback readyCallback;

    public float position { get => transform.position.y; set => transform.position = new Vector3(transform.position.x, value, transform.position.z); }

    public bool isReady { get => currentState == DishStates.Ready; }
    public bool foodIsGrabbable { get => currentState == DishStates.ShowFood; }

    void Start()
    {
        if (Debug.isDebugBuild && cover == null)
            Debug.LogError("There is no reference to the cover of the dish");
    }

    void Update()
    {
        switch(currentState)
        {
            case DishStates.Serve:
                UpdateServing();
                break;
            case DishStates.Remove:
                UpdateRemoving();
                break;
        }
    }

    private void ResetDish()
    {
        position = start;
        cover.SetActive(true);
        food.SetActive(true);
    }

    void UpdateServing()
    {
        bool reachedDestination = SmoothMoveTo(servePosition);

        if (reachedDestination)
        {
            ChangeState(DishStates.Ready);
            position = servePosition;
            readyCallback();
        }
    }

    void UpdateRemoving()
    {
        bool reachedDestination = SmoothMoveTo(end);

        if (reachedDestination)
        {
            ChangeState(DishStates.Wait);
            ResetDish();
        }
    }

    public void Serve()
    {
        ChangeState(DishStates.Serve);
    }

    public void Remove()
    {
        if (currentState != DishStates.Wait)
            ChangeState(DishStates.Remove);
    }

    public void ShowFood()
    {
        ChangeState(DishStates.ShowFood);
        cover.SetActive(false);
    }

    public GameObject GrabFood()
    {
        food.SetActive(false);
        return food;
    }

    bool SmoothMoveTo(float destination)
    {
        float deltaSpeed = speed * Time.fixedDeltaTime;
        float diff = position - destination;

        transform.Translate(0, diff / speed, 0);

        return position <= destination + 0.014f;
    }

    void ChangeState(DishStates newState)
    {
        currentState = newState;
    }
}
