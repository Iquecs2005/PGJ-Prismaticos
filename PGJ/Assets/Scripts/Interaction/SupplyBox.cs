using System.Collections;
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

    private void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
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
            yield return null;
        }

        SpawnItems();
        Unlock();

        used = true;
        cooldownTimer = reuseCooldown;
        isBusy = false;
        routine = null;
        onInteractComplete?.Invoke();

        if (singleUse) Destroy(gameObject);
    }

    private void Unlock()
    {
        if (lockedPlayer != null) lockedPlayer.SetMovementLocked(false);
        lockedPlayer = null;
    }

    private void SpawnItems()
    {
        if (itemPrefab == null) return;

        Vector3 origin = spawnPoint != null ? spawnPoint.position : transform.position;

        for (int i = 0; i < amount; i++)
        {
            Vector2 offset = Random.insideUnitCircle * spawnSpread;
            GameObject spawned = Instantiate(itemPrefab, origin + (Vector3)offset, Quaternion.identity);

            Comida comida = spawned.GetComponent<Comida>();
            if (comida != null) comida.Launch();
        }
    }
}