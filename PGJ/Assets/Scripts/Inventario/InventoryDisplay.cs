using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private ItemDisplay[] slots;

    private DynamicInventory playerInventory;

    private void Start()
    {
        playerInventory = GameManager.playerController.GetComponent<DynamicInventory>();

        if (playerInventory != null)
        {
            playerInventory.OnChanged.AddListener(UpdateInventory);
            UpdateInventory();
        }
    }

    private void OnDestroy()
    {
        playerInventory?.OnChanged.RemoveListener(UpdateInventory);
    }

    public void UpdateInventory()
    {
        int slotIndex = 0;

        foreach (InventoryItem item in playerInventory.Items)
        {
            if (slotIndex >= slots.Length)
            {
                Debug.LogError("To little slots for inventory items");
                return;
            }

            slots[slotIndex].SetItem(item.type, item.amount);

            slotIndex++;
        }

        for (int i = slotIndex; i < slots.Length; i++)
        {
            slots[i].Clear();
        }
    }
}