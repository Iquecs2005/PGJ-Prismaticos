using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
    [SerializeField] protected Collider2D hitboxCollider;
    [SerializeField] protected bool destroyOnHit = false;

    protected int damage = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollisionHit(collision);
    }

    public virtual void SetUp(int damage, LayerMask hittableLayers) 
    {
        this.damage = damage;
        hitboxCollider.includeLayers = hittableLayers;
    }

    protected virtual void HandleCollisionHit(Collider2D collision)
    {
        HealthController hc = collision.GetComponent<HealthController>();
        if (hc != null)
            hc = collision.GetComponentInParent<HealthController>();

        if (hc != null)
            hc.TakeDamage(damage);

        if (destroyOnHit)
            Destroy(gameObject);
    }
}
