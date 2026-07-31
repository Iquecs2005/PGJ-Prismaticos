using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_SFX", menuName = "ScriptableObjects/Audio/SFX")]
public class SFXGroup : ScriptableObject
{
    [SerializeField] private SFXData[] sfxVariations;

    public SFXData GetSFXData() 
    {
        int index = Random.Range(0, sfxVariations.Length);
        return sfxVariations[index];
    }
}

[System.Serializable]
public class SFXData 
{
    public AudioClip clip;
    [Range(0, 1)]
    public float volume = 0.5f;
    public float minPitchShift;
    public float maxPitchShift;
}