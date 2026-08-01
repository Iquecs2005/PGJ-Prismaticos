using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Comida : MonoBehaviour, ICollectible
{
    public static UnityEvent OnFoodCollected;

    public void Collect()
    {
        Debug.Log("Pegou a comida");
        Destroy(gameObject);
        OnFoodCollected?.Invoke();
    }
}