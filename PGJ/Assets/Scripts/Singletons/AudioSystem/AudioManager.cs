using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    public static SFXManager sfxManager;

    [SerializeField] private AudioMixer audioMixer;

    private const string MASTER_KEY = "MasterVolume";
    private const string SFX_KEY = "SFXVolume";
    private const string MUSIC_KEY = "MusicVolume";

    private const float minVolume = -80;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;

        sfxManager = GetComponentInChildren<SFXManager>();
    }

    private void Start() 
    {
        LoadValues();
    }

    public static float GetMasterVolume() 
    {
        return instance.GetMixerVolume(MASTER_KEY);
    }

    public static void SetMasterVolume(float ratio) 
    {
        float volume = instance.CalculateVolume(ratio);
        instance.SetMixerVolume(MASTER_KEY, volume);
    }

    public static float GetSFXVolume()
    {
        return instance.GetMixerVolume(SFX_KEY);
    }

    public static void SetSFXVolume(float ratio) 
    {
        float volume = instance.CalculateVolume(ratio);
        instance.SetMixerVolume(SFX_KEY, volume);
    }

    public static float GetMusicVolume()
    {
        return instance.GetMixerVolume(MUSIC_KEY);
    }

    public static void SetMusicVolume(float ratio) 
    {
        float volume = instance.CalculateVolume(ratio);
        instance.SetMixerVolume(MUSIC_KEY, volume);
    }

    private float GetMixerVolume(string key) 
    {
        instance.audioMixer.GetFloat(key, out float volume);

        if (Mathf.Approximately(volume, minVolume))
            return 0;
        return Mathf.Pow(10, volume / 20f);
    }

    private void SetMixerVolume(string key, float volume) 
    {
        audioMixer.SetFloat(key, volume);
        PlayerPrefs.SetFloat(key, volume);
    }

    private float CalculateVolume(float ratio) 
    {
        if (ratio == 0)
            return minVolume;

        //float newVolume = minVolume + (maxVolume - minVolume) * ratio;
        //return Mathf.Clamp(newVolume, minVolume, maxVolume);

        return Mathf.Log10(ratio) * 20;
    }

    private void LoadValues() 
    {
        LoadMixer(MASTER_KEY);
        LoadMixer(SFX_KEY);
        LoadMixer(MUSIC_KEY);
    }

    private void LoadMixer(string key) 
    {
        float savedVolume = PlayerPrefs.GetFloat(key, CalculateVolume(0.5f));
        audioMixer.SetFloat(key, savedVolume);
    }
}
