using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class PlayerAttack : MonoBehaviour
{
    [Header("Harpoon")]
    [SerializeField] private HarpoonProjectile harpoonPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float harpoonSpeed = 18f;
    [SerializeField] private int harpoonDamage = 5;
    [SerializeField] private float harpoonCooldown = 0.35f;

    [Header("Harpoon Ammo")]
    [SerializeField] private int startingAmmo = 3;

    [Header("Knife")]
    [SerializeField] private float knifeRange = 1.2f;
    [SerializeField] private float knifeRadius = 0.6f;
    [SerializeField] private int knifeDamage = 3;
    [SerializeField] private float knifeCooldown = 0.4f;
    [SerializeField] private LayerMask hittableLayers = ~0;

    public int CurrentAmmo { get; private set; }

    private PlayerController controller;
    private float harpoonCdTimer;
    private float knifeCdTimer;

    void Awake()
    {
        controller = GetComponent<PlayerController>();
        CurrentAmmo = startingAmmo;
    }

    void Update()
    {
        if (harpoonCdTimer > 0f) harpoonCdTimer -= Time.deltaTime;
        if (knifeCdTimer > 0f) knifeCdTimer -= Time.deltaTime;
    }

    public void AddAmmo(int amount)
    {
        if (amount > 0) CurrentAmmo += amount;
    }

    public void FireHarpoon(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (harpoonCdTimer > 0f || CurrentAmmo <= 0 || harpoonPrefab == null) return;

        Vector2 dir = controller.AimDirection();
        controller.FaceTowards(dir.x);

        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        HarpoonProjectile harpoon = Instantiate(harpoonPrefab, spawnPos, Quaternion.Euler(0f, 0f, angle));
        harpoon.Launch(dir, harpoonSpeed, harpoonDamage, controller.Col);

        CurrentAmmo--;
        harpoonCdTimer = harpoonCooldown;
    }

    public void SwingKnife(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (knifeCdTimer > 0f) return;

        Vector2 dir = controller.AimDirection();
        controller.FaceTowards(dir.x);

        Vector2 hitCenter = (Vector2)transform.position + dir * knifeRange;
        Collider2D[] hits = Physics2D.OverlapCircleAll(hitCenter, knifeRadius, hittableLayers);
        foreach (Collider2D hit in hits)
        {
            if (hit == controller.Col) continue;
            HealthController hc = hit.GetComponent<HealthController>()
                                  ?? hit.GetComponentInParent<HealthController>();
            if (hc != null) hc.TakeDamage(knifeDamage);
        }

        knifeCdTimer = knifeCooldown;
    }

    void OnDrawGizmosSelected()
    {
        Vector3 dir = (Application.isPlaying && controller != null)
            ? (Vector3)controller.AimDirection()
            : transform.right;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + dir * knifeRange, knifeRadius);
    }
}