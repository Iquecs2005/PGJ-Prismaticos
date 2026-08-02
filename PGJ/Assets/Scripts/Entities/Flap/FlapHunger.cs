using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlapHunger : MonoBehaviour
{
    [SerializeField] private float maxHunger;
    [SerializeField] private float hungerPerSecond;

    private float currentHunger = 0;

    private bool dead;

    private void FixedUpdate()
    {
        ApplyHunger();
    }

    public void Feed(float amount) 
    {
        if (dead) 
            return;

        currentHunger -= amount;
        currentHunger = Mathf.Max(0, currentHunger);
    }

    private void ApplyHunger() 
    {
        if (dead)
            return;

        currentHunger += Time.deltaTime * hungerPerSecond;
        if (currentHunger > maxHunger)
        {
            OnStarvation();
        }
    }

    private void OnStarvation() 
    {
        print("Flap starved");
        dead = true;
    }
}
