using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject painelPause;

    [Header("Cenas")]
    [SerializeField] private string cenaMenuPrincipal = "MainMenu";

    public static bool IsPaused { get; private set; }

    private void Start()
    {
        if (painelPause != null) painelPause.SetActive(false);
        DefinirPause(false);
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
        if (painelPause != null) painelPause.SetActive(pausar);
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
    private void OnDestroy()
    {
        if (IsPaused) Time.timeScale = 1f;
    }
}