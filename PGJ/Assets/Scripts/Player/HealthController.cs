using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 20;

    public int MaxHealth => maxHealth;
    public int CurrentHealth { get; protected set; }
    public bool IsDead => CurrentHealth <= 0;

    public UnityEvent<int> OnDamageTaken;   
    public event Action<int> OnHealed;       
    public event Action OnDeath;

    protected virtual void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public virtual void TakeDamage(int amount)
    {
        if (amount <= 0 || IsDead) return;

        CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
        OnDamageTaken?.Invoke(amount);

        if (CurrentHealth <= 0)
            Die();
    }

    public virtual void Heal(int amount)
    {
        if (amount <= 0 || IsDead) return;

        CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);
        OnHealed?.Invoke(amount);
    }

    protected virtual void Die()
    {
        OnDeath?.Invoke();
        gameObject.SetActive(false);
    }
}