using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Music", menuName = "ScriptableObjects/Audio/Music")]
public class MusicData : ScriptableObject
{
    public AudioClip clip;
    [Range(0, 1)]
    public float volume = 0.5f;
}
