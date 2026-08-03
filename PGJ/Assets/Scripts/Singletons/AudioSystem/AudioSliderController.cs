using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSliderController : MonoBehaviour
{
    enum AudioSliderType
    {
        Master, SFX, Music
    }

    [SerializeField] private Slider slider;
    [SerializeField] private AudioSliderType sliderType;
    [SerializeField] private float stepSize = 0.1f;

    public void Start()
    {
        SyncValues();
    }

    public void OnSliderButtonPress(float modifier)
    {
        slider.value += stepSize * modifier;
    }

    public void OnValueChange(float value) 
    {
        switch (sliderType)
        {
            case AudioSliderType.Master:
                AudioManager.SetMasterVolume(value);
                break;
            case AudioSliderType.SFX:
                AudioManager.SetSFXVolume(value);
                break;
            case AudioSliderType.Music:
                AudioManager.SetMusicVolume(value);
                break;
        }
    }

    private void SyncValues() 
    {
        float sliderValue = 0;

        switch (sliderType)
        {
            case AudioSliderType.Master:
                sliderValue = AudioManager.GetMasterVolume();
                break;
            case AudioSliderType.SFX:
                sliderValue = AudioManager.GetSFXVolume();
                break;
            case AudioSliderType.Music:
                sliderValue = AudioManager.GetMusicVolume();
                break;
        }

        slider.SetValueWithoutNotify(sliderValue);
    }
}