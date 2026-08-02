using System;
using UnityEngine;
public class Comida : MonoBehaviour, ICollectible, IItemSource
{
    public static event Action OnComidaCollected;

    [Header("Item")]
    [SerializeField] private ItemData item;

    [Header("Saida da caixa")]
    [SerializeField] private float launchSpeed = 5f;
    [SerializeField] private float launchDrag = 4f;
    [SerializeField] private float pickupDelay = 0.7f;
    [SerializeField] private float spawnScale = 1f;

    private Vector2 velocity;
    private float noPickupTimer;
    private bool launched;

    public ItemData Item => item;
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
    public bool Collect()
    {
        if (noPickupTimer > 0f) return false;

        Destroy(gameObject);
        OnComidaCollected?.Invoke();
        return true;
    }
}