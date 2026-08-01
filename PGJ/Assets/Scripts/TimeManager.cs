using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private int startingHour;
    [SerializeField] private int endingHour;
    [SerializeField] private int stepDurationMinute;
    [SerializeField] private float tickDuration;

    private int currentHour = 0;
    private int currentMinute = 0;
    private float currentTickTime = 0;

    private void Start()
    {
        currentHour = startingHour;
    }

    private void Update()
    {
        currentTickTime += Time.deltaTime;
        if (currentTickTime >= tickDuration) 
        {
            OnTick();
        }
    }

    public string GetTime() 
    {
        return $"{currentHour:D2}:{currentMinute:D2}";
    }

    private void OnTick() 
    {
        currentTickTime %= tickDuration;
        currentMinute += stepDurationMinute;
        currentHour += currentMinute / 60;
        currentMinute %= 60;
    }
}
