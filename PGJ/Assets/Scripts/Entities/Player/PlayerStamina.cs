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

    [Header("Events")]
    [SerializeField] private UnityEvent<float, float> onStaminaChanged;

    public float CurrentStamina { get; private set; }
    public float MaxStamina => maxStamina;
    public bool IsSprinting { get; private set; }

    public float SpeedMultiplier => IsSprinting ? sprintMultiplier : 1f;

    public float SprintMultiplier
    {
        get => sprintMultiplier;
        set => sprintMultiplier = Mathf.Max(1f, value);
    }

    private bool sprintInputHeld;
    private float regenTimer;
    private float currentMaxStamina;

    private void Start()
    {
        currentMaxStamina = maxStamina;
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
            else if (CurrentStamina < currentMaxStamina)
            {
                CurrentStamina += regenPerSecond * Time.deltaTime;
                CurrentStamina = Mathf.Min(currentMaxStamina, CurrentStamina);
                onStaminaChanged?.Invoke(CurrentStamina, maxStamina);
            }
        }
    }

    public void SetCurrentMaxStamina(float currentHunger, float maxHunger) 
    {
        float ratio = 1 - (currentHunger / maxHunger);
        currentMaxStamina = maxStamina * ratio;
        CurrentStamina = Mathf.Min(CurrentStamina, currentMaxStamina);
    }
}