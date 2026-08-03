using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [Header("GameOver References")]
    [SerializeField] private GameObject holder;
    [SerializeField] private Image backgroundPanel;

    [Header("GameOver Variables")]
    [SerializeField] private Sprite playerDeathImage;
    [SerializeField] private Sprite flapDeathImage;
    [SerializeField] private string mainMenuScene;

    private bool isGameOver;

    private void Start()
    {
        if (holder != null)
            holder.SetActive(false);
    }

    public void OnGameOver(GameOverType gameOverType) 
    {
        switch (gameOverType)
        {
            case GameOverType.FlapStarved:
                ShowGameOver(flapDeathImage);
                break;
            case GameOverType.JackStarved: 
                ShowGameOver(playerDeathImage);
                break;
            case GameOverType.JackWasEaten:
                ShowGameOver(playerDeathImage);
                break;
        }
    }

    public void MainMenu() 
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void Restart() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ShowGameOver(Sprite deathImage)
    {
        if (isGameOver)
            return;

        backgroundPanel.sprite = deathImage;
        isGameOver = true;

        holder.SetActive(true);

        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (!isGameOver)
            return;

        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
            Restart();
    }
}

public enum GameOverType 
{
    FlapStarved, JackStarved, JackWasEaten
}