using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida : MonoBehaviour, ICollectible
{
    public static event Action OnComidaCollected;

    [Header("Item")]
    [SerializeField] private ItemType item;

    [Header("Saida da caixa")]
    [SerializeField] private float launchSpeed = 5f;
    [SerializeField] private float launchDrag = 4f;
    [SerializeField] private float pickupDelay = 0.7f;
    [SerializeField] private float spawnScale = 1f;

    [Header("Som")]
    [SerializeField] private SFXGroup collectSFX;

    private Vector2 velocity;
    private float noPickupTimer;
    private bool launched;

    public ItemType Item => item;
    public bool CanBeCollected => noPickupTimer <= 0f;

    public void Launch()
    {
        if (spawnScale != 1f)
            transform.localScale *= spawnScale;

        noPickupTimer = pickupDelay;

        float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2f);
        velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * launchSpeed;
        launched = true;
    }
    private void Update()
    {
        if (noPickupTimer > 0f)
            noPickupTimer -= Time.deltaTime;

        if (launched)
        {
            transform.position += (Vector3)(velocity * Time.deltaTime);
            velocity *= Mathf.Clamp01(1f - launchDrag * Time.deltaTime);
            if (velocity.sqrMagnitude < 0.0001f)
                launched = false;
        }
    }
    public bool TryCollect(ref ItemType itemCollected, ref int amount)
    {
        if (noPickupTimer > 0f)
            return false;

        itemCollected = item;
        amount = 1;

        return true;
    }

    public void OnCollect()
    {
        if (collectSFX != null)
            SFXManager.PlaySFX(collectSFX, transform.position);

        Destroy(gameObject);
        OnComidaCollected?.Invoke();
    }
}