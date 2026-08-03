using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Cutscene : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject cutscenePanel;
    [SerializeField] private Image displayImage;

    [Header("Imagens da cutscene")]
    [SerializeField] private Sprite[] images;

    [Header("Cena")]
    [SerializeField] private string gameSceneName = "MainScene";

    private int index;
    private bool active;
    private bool skipThisFrame;

    private void Awake()
    {
        if (cutscenePanel != null)
            cutscenePanel.SetActive(false);
    }
    public void StartCutscene()
    {
        if (images == null || images.Length == 0)
        {
            SceneManager.LoadScene(gameSceneName);
            return;
        }

        active = true;
        index = 0;

        if (cutscenePanel != null)
            cutscenePanel.SetActive(true);

        ShowCurrent();
        skipThisFrame = true;
    }

    private void Update()
    {
        if (!active)
            return;

        if (skipThisFrame)
        {
            skipThisFrame = false;
            return;
        }

        if (AnyPressThisFrame())
            Advance();
    }
    private void Advance()
    {
        index++;

        if (index >= images.Length)
        {
            active = false;
            SceneManager.LoadScene(gameSceneName);
            return;
        }

        ShowCurrent();
    }
    private void ShowCurrent()
    {
        if (displayImage != null && images[index] != null)
            displayImage.sprite = images[index];
    }
    private bool AnyPressThisFrame()
    {
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
            return true;

        if (Mouse.current != null &&
            (Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame))
            return true;

        if (Gamepad.current != null &&
            (Gamepad.current.buttonSouth.wasPressedThisFrame || Gamepad.current.startButton.wasPressedThisFrame))
            return true;

        return false;
    }
}