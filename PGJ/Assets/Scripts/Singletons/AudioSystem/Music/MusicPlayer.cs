using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private MusicData music;
    [SerializeField] private bool startOnAwake;

    void Start()
    {
        if (startOnAwake)
            PlayMusic();
    }

    public void PlayMusic() 
    {
        MusicManager.PlayMusic(music);
    }

    public void StopMusic() 
    {
        MusicManager.StopMusic();
    }
}
