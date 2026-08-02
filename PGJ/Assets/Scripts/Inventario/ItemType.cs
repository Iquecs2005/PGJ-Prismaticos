using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Data")]
public class ItemType : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject worldPrefab;
    [TextArea] public string description;

    public int startingAmmo;
    public int startingCondition;
}