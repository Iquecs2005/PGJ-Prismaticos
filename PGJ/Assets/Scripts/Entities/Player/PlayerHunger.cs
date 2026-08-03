using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHunger : MonoBehaviour
{
    [Header("Fome")]
    [SerializeField] private ItemType foodItemType;
    [SerializeField] private float maxHunger = 100f;

    [SerializeField] private float hungerPerSecond = 1f;
    [SerializeField] private float hungerPerFood = 50;

    [field: Header("Events")]
    [field: SerializeField] public UnityEvent<float, float> onHungerChanged { get; private set; }

    [SerializeField] private UnityEvent onStarved;

    public float CurrentHunger { get; private set; }
    public float MaxHunger => maxHunger;

    public float StaminaAvailableFactor => Mathf.Clamp01(1f - CurrentHunger / maxHunger);

    private bool hasStarved;

    private void Start()
    {
        CurrentHunger = 0f;
        onHungerChanged?.Invoke(CurrentHunger, maxHunger);
    }

    private void FixedUpdate()
    {
        AddHunger(hungerPerSecond * Time.deltaTime);
    }

    public void AddHunger(float amount)
    {
        CurrentHunger = Mathf.Clamp(CurrentHunger + amount, 0f, maxHunger);
        onHungerChanged?.Invoke(CurrentHunger, maxHunger);

        if (!hasStarved && CurrentHunger >= maxHunger)
        {
            hasStarved = true;
            GameManager.gameOverManager?.OnGameOver(GameOverType.JackStarved);
            onStarved?.Invoke();
        }
        else if (CurrentHunger < maxHunger)
        {
            hasStarved = false;
        }
    }

    public void Eat()
    {
        DynamicInventory inventory = GameManager.playerController.inventory;
        if (inventory == null)
            return;

        if (inventory.RemoveItem(foodItemType, 1))
        {
            AddHunger(-hungerPerFood);
        }
        else
        {
            print("No food for you");
        }
    }
}