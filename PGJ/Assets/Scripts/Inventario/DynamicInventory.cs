using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DynamicInventory : MonoBehaviour
{
    [Header("Inventory variables")]
    [SerializeField] private int maxItems = 12;
    [field: SerializeField] private List<InventoryItem> items = new List<InventoryItem>();

    [Header("Inventory Events")]
    [SerializeField] public UnityEvent OnChanged;

    public IReadOnlyList<InventoryItem> Items => items;

    private int currentItems = 0;

    public bool AddItem(ItemType itemData, int amount) 
    {
        if (currentItems + amount > maxItems)
            return false;

        InventoryItem item = FindInventoryItem(itemData);
        if (item != null)
        {
            item.amount += amount;
        }
        else 
        { 
            item = new InventoryItem(itemData, amount);
            items.Add(item);
        }

        currentItems += amount;
        OnChanged?.Invoke();

        return true;
    }

    public bool RemoveItem(ItemType itemData, int amount)
    {
        InventoryItem item = FindInventoryItem(itemData);

        if (item.amount < amount)
            return false;

        if (item.amount > amount) 
        {
            item.amount -= amount;
        }
        else 
        {
            items.Remove(item);
        }

        currentItems -= amount;
        OnChanged?.Invoke();

        return true;
    }

    private InventoryItem FindInventoryItem(ItemType itemType) 
    {
        foreach (InventoryItem item in items)
        {
            if (item.type == itemType)
                return item;
        }
        return null;
    }
}

public class InventoryItem 
{
    public ItemType type;
    public int amount;

    public InventoryItem(ItemType type, int amount)
    {
        this.type = type;
        this.amount = amount;
    }
}