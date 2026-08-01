using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private TimeManager _timeManager;

    public static TimeManager timeManager => GetTimeManager();

    private void Awake()
    {
        if (instance != null && instance != this) 
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private static TimeManager GetTimeManager() 
    {
        if (instance._timeManager == null)
            instance._timeManager = FindObjectOfType<TimeManager>();
        return instance._timeManager;
    }
}
