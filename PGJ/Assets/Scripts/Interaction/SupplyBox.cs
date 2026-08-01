using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Caixa configuravel. Ao interagir (tecla E), trava o player, "abre" durante
/// um tempo, spawna 1+ itens coletaveis (prefab de Comida / ICollectible) e
/// libera o player. Pode ser de uso unico ou reutilizavel.
///
/// Requer um Collider2D marcado como "Is Trigger" para o Interactor detectar.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class SupplyBox : MonoBehaviour, IInteractable
{
    [Header("Conteudo")]
    [Tooltip("Prefab do item a spawnar (ex: a Comida).")]
    [SerializeField] private GameObject itemPrefab;
    [Tooltip("De onde os itens saem. Se vazio, usa a posicao da caixa.")]
    [SerializeField] private Transform spawnPoint;
    [Tooltip("Quantos itens spawnar por abertura.")]
    [SerializeField] private int amount = 1;
    [Tooltip("Espalhamento aleatorio do spawn, em unidades.")]
    [SerializeField] private float spawnSpread = 0.4f;

    [Header("Comportamento")]
    [Tooltip("Tempo (s) que a caixa fica 'abrindo' com o player travado.")]
    [SerializeField] private float openDuration = 0.6f;
    [Tooltip("Se true, so pode ser aberta uma vez.")]
    [SerializeField] private bool singleUse = true;
    [Tooltip("Se reutilizavel, espera este tempo (s) antes de poder abrir de novo.")]
    [SerializeField] private float reuseCooldown = 2f;
    [Tooltip("Texto mostrado no prompt de interacao.")]
    [SerializeField] private string prompt = "Abrir caixa";

    [Header("Eventos (ligue som, animacao, particulas...)")]
    [SerializeField] private UnityEvent onOpenStart;
    [SerializeField] private UnityEvent onOpenFinished;

    private bool isBusy;
    private bool used;
    private float cooldownTimer;

    // --- IInteractable ---
    public bool CanInteract => !isBusy && !(singleUse && used) && cooldownTimer <= 0f;
    public string InteractPrompt => prompt;

    public void Interact(GameObject interactor)
    {
        // DEBUG 4: confirma que a caixa recebeu o Interact.
        Debug.Log($"[SupplyBox] Interact chamado. CanInteract={CanInteract} (isBusy={isBusy}, used={used}, cd={cooldownTimer:0.00})", this);

        if (!CanInteract) return;
        StartCoroutine(OpenRoutine(interactor));
    }
    // ---------------------

    private void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }

    private IEnumerator OpenRoutine(GameObject interactor)
    {
        isBusy = true;

        // Trava o movimento do player, se ele tiver PlayerController.
        // GetComponentInParent: funciona mesmo se o Interactor estiver num filho
        // (ex: no objeto "Collider") e o PlayerController na raiz do Player.
        PlayerController pc = interactor != null ? interactor.GetComponentInParent<PlayerController>() : null;
        if (pc != null) pc.SetMovementLocked(true);

        onOpenStart?.Invoke();

        if (openDuration > 0f)
            yield return new WaitForSeconds(openDuration);

        SpawnItems();

        // Libera o player.
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
            // DEBUG 5: confirma o spawn e onde.
            Debug.Log($"[SupplyBox] Spawnou '{spawned.name}' em {spawned.transform.position}", spawned);
        }
    }
}