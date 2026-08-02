using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaInicial : MonoBehaviour
{
    [Header("Paineis")]
    [SerializeField] private GameObject painelPrincipal;
    [SerializeField] private GameObject painelOpcoes;

    [Header("Musica")]
    [SerializeField] private MusicData musicaMenu;

    [Header("Cena")]
    [SerializeField] private string cenaJogo = "AreaInicial";

    private void Start()
    {
        if (painelOpcoes != null) painelOpcoes.SetActive(false);
        if (painelPrincipal != null) painelPrincipal.SetActive(true);

        if (musicaMenu != null)
            MusicManager.PlayMusic(musicaMenu);
    }
    public void AoBotaoIniciar()
    {
        SceneManager.LoadScene(cenaJogo);
    }
    public void AoBotaoOpcoes()
    {
        painelOpcoes.SetActive(true);
        painelPrincipal.SetActive(false);
    }
    public void AoFecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelPrincipal.SetActive(true);
    }
    public void AoBotaoSair()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}