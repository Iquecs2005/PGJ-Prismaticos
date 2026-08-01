using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private int startingHour;
    [SerializeField] private int endingHour;
    [SerializeField] private int stepDurationMinute;
    [SerializeField] private float tickDuration;

    [Header("Events")]
    [SerializeField] private UnityEvent onTickEvent;
    [SerializeField] private UnityEvent onTimeEndEvent;

    private int currentHour = 0;
    private int currentMinute = 0;
    private float currentTickTime = 0;

    private bool completed = false;

    private void Start()
    {
        currentHour = startingHour;
    }

    private void Update()
    {
        TickTimer();
    }

    public string GetTime() 
    {
        return $"{currentHour:D2}:{currentMinute:D2}";
    }

    private void TickTimer() 
    {
        if (completed) return;

        currentTickTime += Time.deltaTime;
        if (currentTickTime >= tickDuration)
        {
            OnTick();
        }
    }

    private void OnTick() 
    {
        if (completed) return;

        currentTickTime %= tickDuration;
        currentMinute += stepDurationMinute;
        currentHour += currentMinute / 60;
        currentMinute %= 60;

        onTickEvent.Invoke();

        if (currentHour >= endingHour)
        {
            currentHour = endingHour;
            currentMinute = 0;
            completed = true;

            onTimeEndEvent.Invoke();
        }
    }
}
