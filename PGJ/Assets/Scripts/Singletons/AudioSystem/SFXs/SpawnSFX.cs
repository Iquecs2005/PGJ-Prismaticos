using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSFX : MonoBehaviour
{
    [SerializeField] private SFXGroup spawnSFX;
    [SerializeField] private bool positional = true;

    private void Start()
    {
        if (spawnSFX == null)
            return;

        if (positional)
            SFXManager.PlaySFX(spawnSFX, transform.position);
        else
            SFXManager.PlaySFX(spawnSFX);
    }
}