using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HarpoonAttack : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    [Header("Arpão")]
    [SerializeField] private HarpoonProjectile harpoonPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speed = 18f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float cooldown = 0.35f;
    [SerializeField] private LayerMask hittableLayers;

    [Header("Munição")]
    [SerializeField] private int startingAmmo = 3;

    public int CurrentAmmo { get; private set; }
    public bool OnCooldown => cdTimer > 0f;

    private float cdTimer;

    private void Start()
    {
        CurrentAmmo = startingAmmo;  
    }

    void Update()
    {
        if (cdTimer > 0f) cdTimer -= Time.deltaTime;
    }
    public void AddAmmo(int amount)
    {
        if (amount > 0) CurrentAmmo += amount;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.started) TryFire();
    }
    public bool TryFire()
    {
        if (OnCooldown || CurrentAmmo <= 0 || harpoonPrefab == null)
            return false;

        Vector2 dir = controller.AimDirection();
        controller.FaceTowards(dir.x);

        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        HarpoonProjectile harpoon = Instantiate(harpoonPrefab, spawnPos, Quaternion.Euler(0f, 0f, angle));
        harpoon.Launch(dir, speed, damage, hittableLayers);

        CurrentAmmo--;
        cdTimer = cooldown;
        return true;
    }
}
