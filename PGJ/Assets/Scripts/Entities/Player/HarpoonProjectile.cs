using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HarpoonProjectile : HitboxController
{
    [Header("Lifetime")]
    [Tooltip("Despawn de segurança se nunca acertar nada. <= 0 = nunca.")]
    [SerializeField] private float maxLifetime = 6f;
    [Tooltip("Quanto tempo fica cravado antes de sumir. <= 0 = para sempre.")]
    [SerializeField] private float lifeAfterStuck = 8f;

    private Rigidbody2D rb;
    //private bool stuck;
    private float timer = -1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (timer < 0f) 
            return;

        timer -= Time.deltaTime;
        if (timer <= 0f) 
            Destroy(gameObject);
    }

    public void Launch(Vector2 direction, float speed, int damage, LayerMask hittableLayers, GameObject damageDealer = null)
    {
        SetUp(damage, hittableLayers, damageDealer);

        rb.gravityScale = 0f;                     
        rb.velocity = direction.normalized * speed;
        rb.angularVelocity = 0f;
        rb.includeLayers = hittableLayers;

        SetLifetime(maxLifetime);
    }

    protected override void HandleCollisionHit(Collider2D collision)
    {
        base.HandleCollisionHit(collision);
        Stick(collision.transform);
    }

    private void Stick(Transform surface)
    {
        //stuck = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.simulated = false;       
        hitboxCollider.enabled = false;

        transform.SetParent(surface, true);

        SetLifetime(lifeAfterStuck);    
    }

    private void SetLifetime(float seconds) => timer = seconds > 0f ? seconds : -1f;
}
