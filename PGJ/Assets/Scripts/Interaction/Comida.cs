using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida : MonoBehaviour, ICollectible
{
    public static event Action OnCoinCollected;

    public void Collect()
    {
        Debug.Log("Pegou a comida");
        Destroy(gameObject);
        OnCoinCollected?.Invoke();
    }
}