using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 20;

    [Header("Eventos")]
    public UnityEvent<int> OnDamageTaken;
    public UnityEvent<int> OnHealed;
    public UnityEvent OnDeath;

    public int MaxHealth => maxHealth;
    public int CurrentHealth { get; protected set; }
    public bool IsDead => initialized && CurrentHealth <= 0;

    private bool initialized;

    protected virtual void Awake() => EnsureInit();

    private void EnsureInit()
    {
        if (initialized) return;
        CurrentHealth = maxHealth;
        initialized = true;
    }
    public virtual void TakeDamage(int amount)
    {
        EnsureInit();
        if (amount <= 0 || IsDead) return;

        CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
        OnDamageTaken?.Invoke(amount);

        if (CurrentHealth <= 0)
            Die();
    }
    public virtual void Heal(int amount)
    {
        EnsureInit();
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
