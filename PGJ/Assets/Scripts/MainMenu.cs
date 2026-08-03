using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Paineis")]
    [SerializeField] private GameObject painelPrincipal;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject painelCreditos;

    [Header("Cena")]
    [SerializeField] private string cenaJogo = "AreaInicial";

    private void Start()
    {
        OpenMainPanel();
    }

    public void AoBotaoIniciar()
    {
        SceneManager.LoadScene(cenaJogo);
    }

    public void OpenMainPanel()
    {
        painelPrincipal.SetActive(true);
        painelOpcoes.SetActive(false);
        painelCreditos.SetActive(false);
    }

    public void OpenOptions()
    {
        painelPrincipal.SetActive(false);
        painelOpcoes.SetActive(true);
        painelCreditos.SetActive(false);
    }

    public void OpenCredits() 
    {
        painelPrincipal.SetActive(false);
        painelOpcoes.SetActive(false);
        painelCreditos.SetActive(true);
    }

    public void AoBotaoSair()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}