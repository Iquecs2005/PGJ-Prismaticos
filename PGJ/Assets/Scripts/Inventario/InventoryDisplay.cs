using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private DynamicInventory inventory;
    [SerializeField] private ItemDisplay[] slots;
    private readonly List<ItemData> types = new List<ItemData>();
    private readonly List<int> counts = new List<int>();

    private void Start()
    {
        if (inventory == null) inventory = FindObjectOfType<DynamicInventory>();

        if (inventory != null)
        {
            inventory.OnChanged += UpdateInventory;
            UpdateInventory();
        }
    }
    private void OnDestroy()
    {
        if (inventory != null) inventory.OnChanged -= UpdateInventory;
    }

    public void UpdateInventory()
    {
        types.Clear();
        counts.Clear();

        foreach (ItemInstance instance in inventory.Items)
        {
            if (instance == null || instance.itemType == null) continue;

            int index = types.IndexOf(instance.itemType);
            if (index >= 0) counts[index]++;
            else
            {
                types.Add(instance.itemType);
                counts.Add(1);
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < types.Count) slots[i].SetItem(types[i], counts[i]);
            else slots[i].Clear();
        }
    }
}