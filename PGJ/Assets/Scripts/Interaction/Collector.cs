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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectible collectible = collision.gameObject.GetComponent<ICollectible>();
        if (collectible == null) return;

        ItemInstance instance = BuildInstance(collision.gameObject);

        if (collectible.Collect() && instance != null && inventory != null)
            inventory.AddItem(instance);
    }
    private ItemInstance BuildInstance(GameObject obj)
    {
        InstanceItemContainer container = obj.GetComponent<InstanceItemContainer>();
        if (container != null && container.item != null) return container.item;

        IItemSource source = obj.GetComponent<IItemSource>();
        if (source != null && source.Item != null) return new ItemInstance(source.Item);

        return null;
    }
}