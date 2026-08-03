using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private TimeManager _timeManager;
    private PlayerController _playerController;
    private CameraController _cameraController;
    private GameOverManager _gameOverManager;
    private WinManager _winManager;

    public static TimeManager timeManager => GetTimeManager();
    public static PlayerController playerController => GetPlayerController();
    public static CameraController cameraController => GetCameraController();
    public static GameOverManager gameOverManager => GetGameOverManager();
    public static WinManager winManager => GetWinManager();

    public static UnityEvent OnGameOverEvent { get; private set; }
    public static UnityEvent OnWinEvent { get; private set; }

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
        if (instance == null)
            return null;

        if (instance._cameraController == null)
            instance._cameraController = FindObjectOfType<CameraController>();
        return instance._cameraController;
    }

    private static GameOverManager GetGameOverManager() 
    {
        if (instance == null)
            return null;

        if (instance._gameOverManager == null)
            instance._gameOverManager = FindObjectOfType<GameOverManager>();
        return instance._gameOverManager;
    }

    private static WinManager GetWinManager()
    {
        if (instance == null)
            return null;

        if (instance._winManager == null)
            instance._winManager = FindObjectOfType<WinManager>();
        return instance._winManager;
    }
}
