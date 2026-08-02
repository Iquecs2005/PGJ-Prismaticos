using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController controller;
    [SerializeField] private GameObject knifeObject;

    [Header("Variables")]
    [SerializeField] private float range = 1.2f;
    [SerializeField] private int damage = 3;
    [SerializeField] private float cooldown = 0.4f;
    [SerializeField] private LayerMask hittableLayers = ~0;
    [SerializeField] private float attackDuration = 0.15f;

    public bool OnCooldown => cdTimer > 0f;

    private float cdTimer;
    private float attackTimer;

    private void Start()
    {
        if (knifeObject != null) 
            knifeObject.SetActive(false);
    }

    void Update()
    {
        if (cdTimer > 0f) cdTimer -= Time.deltaTime;
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f && knifeObject != null)
                knifeObject.SetActive(false);
        }
    }

    public void SwingKnife(Vector2 aimPosition)
    {
        if (OnCooldown) return;

        Vector2 dir = (aimPosition - (Vector2)transform.position).normalized;

        Vector2 hitCenter = (Vector2)transform.position + dir * range;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        knifeObject.transform.SetPositionAndRotation(hitCenter, Quaternion.Euler(0f, 0f, angle));
        knifeObject.GetComponent<HitboxController>().SetUp(damage, hittableLayers, gameObject);
        knifeObject.SetActive(true);

        attackTimer = attackDuration;

        cdTimer = cooldown;
        return;
    }
}
