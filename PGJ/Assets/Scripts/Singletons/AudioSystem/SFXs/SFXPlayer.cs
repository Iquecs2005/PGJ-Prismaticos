using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] private SFXGroup sfxGroup;

    [SerializeField] private bool directional;
    [SerializeField] private Vector2 audioPosition;
    [SerializeField] private Transform audioParent;

    public void Play() 
    {
        if (directional)
            SFXManager.PlaySFX(sfxGroup, audioPosition, audioParent);
        else
            SFXManager.PlaySFX(sfxGroup);
    }
}
