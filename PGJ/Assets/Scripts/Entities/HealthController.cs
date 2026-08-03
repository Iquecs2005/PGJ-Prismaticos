using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 20;

    [Header("I-Frames")]
    [SerializeField] private float invincibilityDuration = 0f;

    [Header("Eventos")]
    public UnityEvent<HitInformation> OnDamageTaken;
    public UnityEvent<int> OnHealed;
    public UnityEvent OnDeath;

    public UnityEvent<int, int> onHealthChanged;

    public UnityEvent OnInvincibilityStarted;

    public int MaxHealth => maxHealth;
    public int CurrentHealth { get; protected set; }
    public bool IsDead => CurrentHealth <= 0;
    public bool IsInvincible => invincibilityTimer > 0f;

    private float invincibilityTimer;

    private void Start()
    {
        CurrentHealth = maxHealth;
        onHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }
    private void Update()
    {
        if (invincibilityTimer > 0f)
            invincibilityTimer -= Time.deltaTime;
    }
    public virtual void TakeDamage(HitInformation hit)
    {
        if (hit.damage <= 0 || IsDead || IsInvincible) return;

        CurrentHealth = Mathf.Max(CurrentHealth - hit.damage, 0);
        OnDamageTaken?.Invoke(hit);
        onHealthChanged?.Invoke(CurrentHealth, maxHealth);

        if (CurrentHealth <= 0)
        {
            Die();
            return;
        }

        if (invincibilityDuration > 0f)
        {
            invincibilityTimer = invincibilityDuration;
            OnInvincibilityStarted?.Invoke();
        }
    }
    public virtual void Heal(int amount)
    {
        if (amount <= 0 || IsDead) return;

        CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);
        OnHealed?.Invoke(amount);
        onHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }
    public virtual void Kill()
    {
        if (IsDead)
            return;
        CurrentHealth = 0;
        onHealthChanged?.Invoke(CurrentHealth, maxHealth);
        Die();
    }
    protected virtual void Die()
    {
        OnDeath?.Invoke();

        Destroy(gameObject);
    }
}