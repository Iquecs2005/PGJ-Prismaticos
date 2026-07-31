using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    public static SFXManager sfxManager;

    void Awake()
    {
        instance = this;

        sfxManager = GetComponentInChildren<SFXManager>();
    }
}
