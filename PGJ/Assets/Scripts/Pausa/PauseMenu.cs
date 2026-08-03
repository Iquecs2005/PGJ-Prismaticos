using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject holder;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;

    [Header("Cenas")]
    [SerializeField] private string cenaMenuPrincipal = "MainMenu";

    public static bool IsPaused { get; private set; }

    private void Start()
    {
        if (holder != null)
            holder.SetActive(false);
        DefinirPause(false);
    }

    private void OnEnable()
    {
        GameManager.playerController.input.onPauseAction.AddListener(TogglePause);
    }

    private void OnDisable()
    {
        GameManager.playerController?.input.onPauseAction.RemoveListener(TogglePause);
    }

    public void TogglePause()
    {
        DefinirPause(!IsPaused);
    }

    public void Continuar()
    {
        DefinirPause(false);
    }

    private void DefinirPause(bool pausar)
    {
        IsPaused = pausar;
        if (holder != null) 
            holder.SetActive(pausar);

        pausePanel.SetActive(pausar);
        settingsPanel.SetActive(false);

        Time.timeScale = pausar ? 0f : 1f;
    }

    public void IrParaMenuPrincipal()
    {
        Time.timeScale = 1f;
        IsPaused = false;
        SceneManager.LoadScene(cenaMenuPrincipal);
    }

    public void Sair()
    {
        Time.timeScale = 1f;
        IsPaused = false;
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OpenSettingsPanel() 
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OpenPausePanel() 
    {
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        if (IsPaused) Time.timeScale = 1f;
    }
}