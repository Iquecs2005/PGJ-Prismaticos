using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Imagens de morte (objetos separados)")]
    [SerializeField] private GameObject playerDeathImage;
    [SerializeField] private GameObject flapDeathImage;

    public static GameOverManager Instance { get; private set; }

    private bool isGameOver;

    private void Awake()
    {
        Instance = this;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (playerDeathImage != null)
            playerDeathImage.SetActive(false);

        if (flapDeathImage != null)
            flapDeathImage.SetActive(false);
    }
    public void OnPlayerStarved()
    {
        ShowGameOver(playerDeathImage);
    }
    public void OnPlayerDied()
    {
        ShowGameOver(playerDeathImage);
    }
    public void OnFlapStarved()
    {
        ShowGameOver(flapDeathImage);
    }
    private void ShowGameOver(GameObject deathImage)
    {
        if (isGameOver)
            return;

        isGameOver = true;

        if (playerDeathImage != null)
            playerDeathImage.SetActive(deathImage == playerDeathImage);

        if (flapDeathImage != null)
            flapDeathImage.SetActive(deathImage == flapDeathImage);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (!isGameOver)
            return;

        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
            Restart();
    }
    private void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}