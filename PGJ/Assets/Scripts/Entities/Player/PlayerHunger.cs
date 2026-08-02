using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHunger : MonoBehaviour
{
    [Header("Fome")]
    [SerializeField] private float maxHunger = 100f;

    [SerializeField] private float hungerPerTick = 1f;

    [Header("Events")]
    [SerializeField] private UnityEvent<float, float> onHungerChanged;

    [SerializeField] private UnityEvent onStarved;

    public float CurrentHunger { get; private set; }
    public float MaxHunger => maxHunger;

    public float StaminaAvailableFactor => Mathf.Clamp01(1f - CurrentHunger / maxHunger);

    private bool hasStarved;

    private void Start()
    {
        CurrentHunger = 0f;
        onHungerChanged?.Invoke(CurrentHunger, maxHunger);

        GameManager.timeManager.onTickEvent.AddListener(OnTick);
    }
    private void OnDestroy()
    {
        if (GameManager.timeManager != null)
            GameManager.timeManager.onTickEvent.RemoveListener(OnTick);
    }
    private void OnTick()
    {
        AddHunger(hungerPerTick);
    }
    public void AddHunger(float amount)
    {
        CurrentHunger = Mathf.Clamp(CurrentHunger + amount, 0f, maxHunger);
        onHungerChanged?.Invoke(CurrentHunger, maxHunger);

        if (!hasStarved && CurrentHunger >= maxHunger)
        {
            hasStarved = true;
            onStarved?.Invoke();
        }
        else if (CurrentHunger < maxHunger)
        {
            hasStarved = false;
        }
    }
    public void Feed(float amount)
    {
        AddHunger(-Mathf.Abs(amount));
    }
}