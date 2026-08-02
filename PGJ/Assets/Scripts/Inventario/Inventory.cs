using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Serializable]
    public class Slot
    {
        public ItemData item;
        public int count;
    }
    [SerializeField] private int maxSlots = 12;
    [SerializeField] private List<Slot> slots = new List<Slot>();

    public IReadOnlyList<Slot> Slots => slots;
    public event Action OnChanged;

    public bool AddItem(ItemData item, int amount = 1)
    {
        if (item == null || amount <= 0) return false;

        Slot existing = slots.Find(s => s.item == item);
        if (existing != null)
        {
            existing.count += amount;
            OnChanged?.Invoke();
            return true;
        }

        if (slots.Count >= maxSlots) return false;

        slots.Add(new Slot { item = item, count = amount });
        OnChanged?.Invoke();
        return true;
    }

    public bool RemoveItem(ItemData item, int amount = 1)
    {
        if (item == null || amount <= 0) return false;

        Slot existing = slots.Find(s => s.item == item);
        if (existing == null) return false;

        existing.count -= amount;
        if (existing.count <= 0) slots.Remove(existing);

        OnChanged?.Invoke();
        return true;
    }
}