using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Comida : MonoBehaviour, ICollectible
{
    public static UnityEvent OnFoodCollected;

    public void Collect()
    {
        Destroy(gameObject);
        OnFoodCollected?.Invoke();
    }
}