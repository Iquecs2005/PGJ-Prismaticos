using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private static SFXLibrary sfxLibrary;

    private void Awake()
    {
        sfxLibrary = GetComponent<SFXLibrary>();
    }

    private void Start()
    {
        Play("Player_Swim");
    }

    public static void Play(string sfxName) 
    {
        sfxLibrary.GetSFXData(sfxName);
    }
}
