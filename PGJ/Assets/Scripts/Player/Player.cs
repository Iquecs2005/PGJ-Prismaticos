using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [Header("Referências (auto-preenchem se vazias; podem estar em filhos)")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Camera cam;

    public Rigidbody2D Rb => rb;
    public Collider2D Col => col;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public PlayerMovement Movement { get; private set; }
    public HarpoonAttack Harpoon { get; private set; }
    public KnifeAttack Knife { get; private set; }
    public PlayerAim Aim { get; private set; }
    public HealthController Health { get; private set; }
    public bool MovementLocked { get; private set; }
    public bool IsFacingRight { get; private set; } = true;
    public bool Initialized { get; private set; }
    private PlayerHub[] modules;
    private float originalScaleX;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        if (!Initialized) Initialize();
    }

    public void Initialize()
    {
        if (Initialized) return;

        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (col == null) col = GetComponentInChildren<Collider2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (cam == null) cam = Camera.main;

        originalScaleX = Mathf.Abs(transform.localScale.x);

        Movement = GetComponentInChildren<PlayerMovement>(true);
        Harpoon = GetComponentInChildren<HarpoonAttack>(true);
        Knife = GetComponentInChildren<KnifeAttack>(true);
        Aim = GetComponentInChildren<PlayerAim>(true);
        Health = GetComponentInChildren<HealthController>(true);

        modules = GetComponentsInChildren<PlayerHub>(true);
        foreach (PlayerHub module in modules)
            module.Init(this);

        Initialized = true;
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
    public Vector2 MouseWorldPosition()
    {
        if (cam == null || Mouse.current == null) return transform.position;

        Vector3 screen = Mouse.current.position.ReadValue();
        screen.z = Mathf.Abs(cam.transform.position.z);
        return cam.ScreenToWorldPoint(screen);
    }

    public Vector2 AimDirection()
    {
        Vector2 dir = MouseWorldPosition() - (Vector2)transform.position;
        return dir.sqrMagnitude > 0.0001f ? dir.normalized : (IsFacingRight ? Vector2.right : Vector2.left);
    }
    public void SetMovementLocked(bool locked) => MovementLocked = locked;
    public void FaceTowards(float xDirection)
    {
        if (Mathf.Abs(xDirection) < 0.01f) return;

        bool faceRight = xDirection > 0f;
        if (faceRight == IsFacingRight) return;

        IsFacingRight = faceRight;
        Vector3 s = transform.localScale;
        s.x = faceRight ? originalScaleX : -originalScaleX;
        transform.localScale = s;
    }
}