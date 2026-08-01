using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class SupplyBox : MonoBehaviour, IInteractable
{
    [Header("Conteudo")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int amount = 1;
    [SerializeField] private float spawnSpread = 0.4f;

    [Header("Comportamento")]
    [SerializeField] private float holdDuration = 0.8f;
    [SerializeField] private bool singleUse = true;
    [SerializeField] private float reuseCooldown = 2f;

    [Header("Saida dos itens")]
    [SerializeField] private float launchSpeed = 5f;
    [SerializeField] private float launchDrag = 4f;
    [SerializeField] private float pickupDelay = 0.7f;
    [SerializeField] private float spawnScale = 1f;

    [Header("Icone")]
    [SerializeField] private GameObject icon;
    [SerializeField] private Transform iconFill;

    [Header("Eventos")]
    [SerializeField] private UnityEvent onInteractStart;
    [SerializeField] private UnityEvent onInteractComplete;
    [SerializeField] private UnityEvent onInteractCancel;

    private bool isBusy;
    private bool used;
    private float cooldownTimer;
    private Coroutine routine;
    private PlayerController lockedPlayer;

    public bool CanInteract => !isBusy && !(singleUse && used) && cooldownTimer <= 0f;

    private void Awake()
    {
        if (icon != null) icon.SetActive(false);
        SetFill(0f);
    }
    private void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }
    public void OnFocusEnter()
    {
        if (icon != null) icon.SetActive(true);
    }
    public void OnFocusExit()
    {
        if (icon != null) icon.SetActive(false);
        SetFill(0f);
    }
    public void StartInteract(GameObject interactor)
    {
        if (!CanInteract) return;
        routine = StartCoroutine(HoldRoutine(interactor));
    }
    public void CancelInteract(GameObject interactor)
    {
        if (!isBusy) return;
        if (routine != null) StopCoroutine(routine);
        routine = null;
        Unlock();
        SetFill(0f);
        isBusy = false;
        onInteractCancel?.Invoke();
    }
    private IEnumerator HoldRoutine(GameObject interactor)
    {
        isBusy = true;
        onInteractStart?.Invoke();

        lockedPlayer = interactor != null ? interactor.GetComponentInParent<PlayerController>() : null;
        if (lockedPlayer != null) lockedPlayer.SetMovementLocked(true);

        float t = 0f;
        while (t < holdDuration)
        {
            t += Time.deltaTime;
            SetFill(t / holdDuration);
            yield return null;
        }

        Unlock();
        SetFill(0f);

        used = true;
        cooldownTimer = reuseCooldown;
        isBusy = false;
        routine = null;
        onInteractComplete?.Invoke();

        if (icon != null) icon.SetActive(false);

        StartCoroutine(LaunchRoutine());
    }
    private IEnumerator LaunchRoutine()
    {
        if (singleUse) ConsumeVisual();

        List<Transform> items = new List<Transform>();
        List<Vector2> velocities = new List<Vector2>();
        List<Collider2D> colliders = new List<Collider2D>();

        Vector3 origin = spawnPoint != null ? spawnPoint.position : transform.position;

        if (itemPrefab != null)
        {
            for (int i = 0; i < amount; i++)
            {
                Vector2 offset = Random.insideUnitCircle * spawnSpread;
                GameObject spawned = Instantiate(itemPrefab, origin + (Vector3)offset, Quaternion.identity);

                if (spawnScale != 1f)
                    spawned.transform.localScale *= spawnScale;

                Collider2D col = spawned.GetComponent<Collider2D>();
                if (col != null) col.enabled = false;

                float angle = Random.Range(0f, Mathf.PI * 2f);

                items.Add(spawned.transform);
                velocities.Add(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * launchSpeed);
                colliders.Add(col);
            }
        }
        float launchTime = Mathf.Max(pickupDelay, 0.6f);
        float t = 0f;
        bool collidersOn = false;

        while (t < launchTime)
        {
            t += Time.deltaTime;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == null) continue;
                items[i].position += (Vector3)(velocities[i] * Time.deltaTime);
                velocities[i] *= Mathf.Clamp01(1f - launchDrag * Time.deltaTime);
            }

            if (!collidersOn && t >= pickupDelay)
            {
                collidersOn = true;
                EnableColliders(colliders);
            }

            yield return null;
        }

        if (!collidersOn)
            EnableColliders(colliders);

        if (singleUse)
            Destroy(gameObject);
    }
    private void EnableColliders(List<Collider2D> colliders)
    {
        foreach (Collider2D c in colliders)
            if (c != null) c.enabled = true;
    }
    private void ConsumeVisual()
    {
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
            sr.enabled = false;

        Collider2D myCol = GetComponent<Collider2D>();
        if (myCol != null) myCol.enabled = false;
    }
    private void Unlock()
    {
        if (lockedPlayer != null) lockedPlayer.SetMovementLocked(false);
        lockedPlayer = null;
    }
    private void SetFill(float value01)
    {
        if (iconFill == null) return;
        Vector3 s = iconFill.localScale;
        s.y = Mathf.Clamp01(value01);
        iconFill.localScale = s;
    }
}