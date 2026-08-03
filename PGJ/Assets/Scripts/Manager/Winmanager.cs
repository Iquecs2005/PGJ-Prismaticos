using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinManager : MonoBehaviour
{
    public static WinManager Instance { get; private set; }

    [Header("Cena de vitoria")]
    [SerializeField] private string victorySceneName = "VictoryScene";

    private bool hasWon;

    private void Awake()
    {
        Instance = this;
    }
    public void Win()
    {
        if (hasWon)
            return;

        hasWon = true;

        Time.timeScale = 1f;
        SceneManager.LoadScene(victorySceneName);
    }
}