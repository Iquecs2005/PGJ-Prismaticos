using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina")]
    [SerializeField] private float maxStamina = 100f;

    [SerializeField] private float drainPerSecond = 25f;

    [SerializeField] private float regenPerSecond = 15f;

    [SerializeField] private float regenDelay = 1f;

    [SerializeField] private float minStaminaToSprint = 5f;

    [Header("Aceleracao")]
    [SerializeField] private float sprintMultiplier = 1.8f;
    
    [Header("Fome")]
    [SerializeField] private PlayerHunger hunger;


    [Header("Events")]
    [SerializeField] private UnityEvent<float, float> onStaminaChanged;

    public float CurrentStamina { get; private set; }
    public float MaxStamina => maxStamina;
    public float EffectiveMaxStamina =>
        hunger != null ? maxStamina * hunger.StaminaAvailableFactor : maxStamina;
    public bool IsSprinting { get; private set; }

    public float SpeedMultiplier => IsSprinting ? sprintMultiplier : 1f;

    public float SprintMultiplier
    {
        get => sprintMultiplier;
        set => sprintMultiplier = Mathf.Max(1f, value);
    }

    private bool sprintInputHeld;
    private float regenTimer;

    private void Start()
    {
        CurrentStamina = maxStamina;
        onStaminaChanged?.Invoke(CurrentStamina, maxStamina);
    }
    public void SetSprinting(bool held)
    {
        sprintInputHeld = held;
    }

    private void Update()
    {
        bool wantsToSprint = sprintInputHeld && CurrentStamina > 0f;

        if (!IsSprinting && wantsToSprint && CurrentStamina < minStaminaToSprint)
            wantsToSprint = false;
        IsSprinting = wantsToSprint;
        if (IsSprinting)
        {
            CurrentStamina -= drainPerSecond * Time.deltaTime;
            CurrentStamina = Mathf.Max(0f, CurrentStamina);
            regenTimer = regenDelay;
            onStaminaChanged?.Invoke(CurrentStamina, maxStamina);
        }
        else
        {
            if (regenTimer > 0f)
            {
                regenTimer -= Time.deltaTime;
            }
            else if (CurrentStamina < maxStamina)
            {
                CurrentStamina += regenPerSecond * Time.deltaTime;
                CurrentStamina = Mathf.Min(maxStamina, CurrentStamina);
                onStaminaChanged?.Invoke(CurrentStamina, maxStamina);
            }
        }
    }
}