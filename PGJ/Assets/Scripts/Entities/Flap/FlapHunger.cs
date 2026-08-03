using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlapHunger : MonoBehaviour
{
    [SerializeField] private float maxHunger;
    [SerializeField] private float hungerPerSecond;
    [SerializeField] private float hungerPerFood;
    [SerializeField] private ItemType foodItemType;

    private float currentHunger = 0;

    private bool dead;

    private void FixedUpdate()
    {
        ApplyHunger();
    }

    public void Feed() 
    {
        if (dead) 
            return;

        DynamicInventory inventory = GameManager.playerController.GetComponent<DynamicInventory>();
        if (inventory == null)
            return;

        if (inventory.RemoveItem(foodItemType, 1)) 
        {
            currentHunger -= hungerPerFood;
            currentHunger = Mathf.Max(0, currentHunger);
        }
        else 
        {
            print("No food for flap");
        }
    }

    public float GetHungerRatio() 
    {
        return currentHunger / maxHunger;
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
