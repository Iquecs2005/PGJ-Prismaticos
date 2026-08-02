using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private TimeManager _timeManager;
    private PlayerController _playerController;
    private CameraController _cameraController;

    public static TimeManager timeManager => GetTimeManager();
    public static PlayerController playerController => GetPlayerController();
    public static CameraController cameraController => GetCameraController();

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
    private static PlayerController GetPlayerController()
    {
        if (instance._playerController == null)
            instance._playerController = FindObjectOfType<PlayerController>();
        return instance._playerController;
    }

    private static CameraController GetCameraController()
    {
        if (instance._cameraController == null)
            instance._cameraController = FindObjectOfType<CameraController>();
        return instance._cameraController;
    }
}
