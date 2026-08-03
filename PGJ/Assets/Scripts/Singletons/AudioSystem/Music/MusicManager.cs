using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] private AudioSource musicSource;

    private static string currentMusic;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }

    public static void PlayMusic(MusicData musicData, bool loop = true)
    {
        if (instance == null)
        {
            Debug.LogError("MusicManager.PlayMusic: No MusicManager on scene. Please put GameManager Prefab on scene");
            return;
        }
        if (musicData == null)
        {
            Debug.LogError("MusicManager.PlayMusic: Trying to play Null Music Data");
            return;
        }

        string musicName = musicData.name;

        if (musicName == currentMusic)
            return;

        AudioSource audioSource = instance.musicSource;

        StopMusic();

        currentMusic = musicData.name;
        audioSource.clip = musicData.clip;
        audioSource.volume = musicData.volume;
        audioSource.loop = loop;

        audioSource.Play();
    }

    public static void StopMusic() 
    {
        if (instance == null)
        {
            Debug.LogError("MusicManager.PlayMusic: No MusicManager on scene. Please put GameManager Prefab on scene");
            return;
        }

        instance.musicSource.Stop();

        instance.musicSource.clip = null;

        currentMusic = "";
    }
}
