using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HarpoonProjectile : MonoBehaviour
{
    [Header("Lifetime")]
    [Tooltip("Despawn de segurança se nunca acertar nada. <= 0 = nunca.")]
    [SerializeField] private float maxLifetime = 6f;
    [Tooltip("Quanto tempo fica cravado antes de sumir. <= 0 = para sempre.")]
    [SerializeField] private float lifeAfterStuck = 8f;

    private Rigidbody2D rb;
    private Collider2D col;
    private int damage;
    private bool stuck;
    private float timer = -1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void Launch(Vector2 direction, float speed, int damage, LayerMask hittableLayers)
    {
        this.damage = damage;

        rb.gravityScale = 0f;                     
        rb.velocity = direction.normalized * speed;
        rb.angularVelocity = 0f;

        rb.includeLayers = hittableLayers;

        SetLifetime(maxLifetime);
    }

    void Update()
    {
        if (timer < 0f) return;
        timer -= Time.deltaTime;
        if (timer <= 0f) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision) => HandleHit(collision.collider);
    void OnTriggerEnter2D(Collider2D other) => HandleHit(other);

    private void HandleHit(Collider2D other)
    {
        if (stuck) return;

        HealthController hc = other.GetComponent<HealthController>()
                              ?? other.GetComponentInParent<HealthController>();
        if (hc != null) hc.TakeDamage(damage);

        Stick(other.transform);
    }

    private void Stick(Transform surface)
    {
        stuck = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.simulated = false;       
        if (col != null) col.enabled = false;

        transform.SetParent(surface, true);

        SetLifetime(lifeAfterStuck);    
    }
    private void SetLifetime(float seconds) => timer = seconds > 0f ? seconds : -1f;
}
