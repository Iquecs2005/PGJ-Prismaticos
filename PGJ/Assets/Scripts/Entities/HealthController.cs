using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 20;

    [Header("Eventos")]
    public UnityEvent<HitInformation> OnDamageTaken;
    public UnityEvent<int> OnHealed;
    public UnityEvent OnDeath;

    public int MaxHealth => maxHealth;
    public int CurrentHealth { get; protected set; }
    public bool IsDead => CurrentHealth <= 0;

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public virtual void TakeDamage(HitInformation hit)
    {
        print($"{hit.damage} from {hit.damageDealer} by {hit.damageOrigin}");

        if (hit.damage <= 0 || IsDead) return;

        CurrentHealth = Mathf.Max(CurrentHealth - hit.damage, 0);
        OnDamageTaken?.Invoke(hit);

        if (CurrentHealth <= 0)
            Die();
    }

    public virtual void Heal(int amount)
    {
        if (amount <= 0 || IsDead) return;

        CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);
        OnHealed?.Invoke(amount);
    }
    public virtual void Kill()
    {
        if (IsDead)
            return;
        CurrentHealth = 0;
        Die();
    }

    protected virtual void Die()
    {
        OnDeath?.Invoke();
        gameObject.SetActive(false);
    }
}
