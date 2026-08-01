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
    [SerializeField] private float openDuration = 0.6f;
    [SerializeField] private bool singleUse = true;
    [SerializeField] private float reuseCooldown = 2f;
    [SerializeField] private string prompt = "Abrir caixa";

    [Header("Eventos")]
    [SerializeField] private UnityEvent onOpenStart;
    [SerializeField] private UnityEvent onOpenFinished;

    private bool isBusy;
    private bool used;
    private float cooldownTimer;

    public bool CanInteract => !isBusy && !(singleUse && used) && cooldownTimer <= 0f;
    public string InteractPrompt => prompt;

    public void Interact(GameObject interactor)
    {
        Debug.Log($"[SupplyBox] Interact chamado. CanInteract={CanInteract} (isBusy={isBusy}, used={used}, cd={cooldownTimer:0.00})", this);

        if (!CanInteract) return;
        StartCoroutine(OpenRoutine(interactor));
    }

    private void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }

    private IEnumerator OpenRoutine(GameObject interactor)
    {
        isBusy = true;
        PlayerController pc = interactor != null ? interactor.GetComponentInParent<PlayerController>() : null;
        if (pc != null) pc.SetMovementLocked(true);

        onOpenStart?.Invoke();

        if (openDuration > 0f)
            yield return new WaitForSeconds(openDuration);

        SpawnItems();
        if (pc != null) pc.SetMovementLocked(false);

        onOpenFinished?.Invoke();

        used = true;
        cooldownTimer = reuseCooldown;
        isBusy = false;

        if (singleUse)
            Destroy(gameObject);
    }

    private void SpawnItems()
    {
        if (itemPrefab == null)
        {
            Debug.LogWarning($"[SupplyBox] '{name}' sem itemPrefab configurado.", this);
            return;
        }

        Vector3 origin = spawnPoint != null ? spawnPoint.position : transform.position;

        for (int i = 0; i < amount; i++)
        {
            Vector2 offset = Random.insideUnitCircle * spawnSpread;
            GameObject spawned = Instantiate(itemPrefab, origin + (Vector3)offset, Quaternion.identity);
            Debug.Log($"[SupplyBox] Spawnou '{spawned.name}' em {spawned.transform.position}", spawned);
        }
    }
}