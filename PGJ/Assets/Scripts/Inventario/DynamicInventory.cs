using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DynamicInventory : MonoBehaviour
{
    [SerializeField] private int maxItems = 12;
    [SerializeField] private List<ItemInstance> items = new List<ItemInstance>();
    public IReadOnlyList<ItemInstance> Items => items;
    public event Action OnChanged;
    public bool AddItem(ItemInstance itemToAdd)
    {
        if (itemToAdd == null) return false;

        if (items.Count >= maxItems)
        {
            Debug.Log("No space in the inventory");
            return false;
        }
        items.Add(itemToAdd);
        OnChanged?.Invoke();
        return true;
    }
    public void RemoveAt(int index)
    {
        if (index < 0 || index >= items.Count) return;
        items.RemoveAt(index);
        OnChanged?.Invoke();
    }
}