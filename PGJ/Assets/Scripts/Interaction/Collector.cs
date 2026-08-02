using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private DynamicInventory inventory;

    private void Awake()
    {
        if (inventory == null) inventory = GetComponentInParent<DynamicInventory>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        ICollectible collectible = collision.gameObject.GetComponent<ICollectible>();
        CollectItem(collectible);
    }

    private void CollectItem(ICollectible collectible) 
    {
        if (collectible == null)
            return;

        ItemType itemData = null;
        int amount = 0;

        if (collectible.TryCollect(ref itemData, ref amount))
        {
            if (inventory == null)
                return;
            if (inventory.AddItem(itemData, amount))
                collectible.OnCollect();
        }
    }
}