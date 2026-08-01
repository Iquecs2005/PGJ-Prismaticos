using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnifeAttack : PlayerHub
{
    [Header("Faca")]
    [SerializeField] private float range = 1.2f;
    [SerializeField] private float radius = 0.6f;
    [SerializeField] private int damage = 3;
    [SerializeField] private float cooldown = 0.4f;
    [SerializeField] private LayerMask hittableLayers = ~0;

    [Header("Visual")]
    [SerializeField] private GameObject knifeVisual;
    [SerializeField] private float visualDuration = 0.15f;
    public bool OnCooldown => cdTimer > 0f;

    private float cdTimer;
    private float visualTimer;

    protected override void OnInit()
    {
        if (knifeVisual != null) knifeVisual.SetActive(false);
    }

    void Update()
    {
        if (cdTimer > 0f) cdTimer -= Time.deltaTime;
        if (visualTimer > 0f)
        {
            visualTimer -= Time.deltaTime;
            if (visualTimer <= 0f && knifeVisual != null)
                knifeVisual.SetActive(false);
        }
    }
    public void Swing(InputAction.CallbackContext context)
    {
        if (context.started) TrySwing();
    }

    public bool TrySwing()
    {
        if (!Initialized || OnCooldown) return false;

        Vector2 dir = player.AimDirection();
        player.FaceTowards(dir.x);

        Vector2 hitCenter = (Vector2)player.transform.position + dir * range;

        if (knifeVisual != null)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            knifeVisual.transform.SetPositionAndRotation(hitCenter, Quaternion.Euler(0f, 0f, angle));
            knifeVisual.SetActive(true);
            visualTimer = visualDuration;
        }
        Collider2D[] hits = Physics2D.OverlapCircleAll(hitCenter, radius, hittableLayers);
        foreach (Collider2D hit in hits)
        {
            if (hit == player.Col) continue;
            HealthController hc = hit.GetComponent<HealthController>()
                                  ?? hit.GetComponentInParent<HealthController>();
            if (hc != null) hc.TakeDamage(damage);
        }

        cdTimer = cooldown;
        return true;
    }
    void OnDrawGizmosSelected()
    {
        Vector3 dir = (Application.isPlaying && Initialized)
            ? (Vector3)player.AimDirection()
            : transform.right;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + dir * range, radius);
    }
}
