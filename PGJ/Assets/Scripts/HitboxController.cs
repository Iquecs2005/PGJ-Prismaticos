using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
    [SerializeField] protected Collider2D hitboxCollider;
    [SerializeField] private HitInformation hitInfo;
    [SerializeField] protected bool destroyOnHit = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollisionHit(collision);
    }

    public virtual void SetUp(int damage, LayerMask hittableLayers, GameObject damageDealer = null) 
    {
        hitInfo.damage = damage;
        hitInfo.damageOrigin = gameObject;
        if (damageDealer != null) 
        {
            hitInfo.damageDealer = damageDealer;
        }
        else 
        {
            if (hitInfo.damageDealer == null)
                hitInfo.damageDealer = gameObject;
        }
        hitboxCollider.includeLayers = hittableLayers;
    }

    protected virtual void HandleCollisionHit(Collider2D collision)
    {
        HealthController hc = collision.GetComponent<HealthController>();
        if (hc == null)
            hc = collision.GetComponentInParent<HealthController>();

        if (hc != null)
            hc.TakeDamage(hitInfo);

        if (destroyOnHit)
            Destroy(gameObject);
    }
}
