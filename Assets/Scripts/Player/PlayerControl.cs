using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public float MoveSpeed = 3f;

    public SpriteRenderer sr;
    public InputAction MoveAction;
    public InputAction FireAction;
    public Animator animator;

    public PlayerDash playerDash;

    private Vector2 lastMoveDirection = Vector2.down;
    private Rigidbody2D rb;
    public Vector2 MoveInput { get; private set; }
    public Vector2 LastMoveDirection => lastMoveDirection;
    public InputAction Using;
    void Awake()
    {
        Instance = this;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (playerDash == null)
            playerDash = GetComponent<PlayerDash>();
    }

    void OnEnable()
    {
        Using.Enable();
        MoveAction.Enable();
        FireAction.Enable();
    }

    void OnDisable()
    {
        Using.Enable();
        MoveAction.Disable();
        FireAction.Disable();
    }

    void FixedUpdate()
    {
        // если игрок делает dash — обычное движение НЕ работает
        if (playerDash != null && playerDash.IsDashing)
            return;

        MoveInput = MoveAction.ReadValue<Vector2>();
        Vector2 move = MoveInput;

        Vector2 position = rb.position + move * MoveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(position);

        if (move.sqrMagnitude > 0.01f)
        {
            lastMoveDirection = move.normalized;

            animator.SetFloat("Horizontal", lastMoveDirection.x);
            animator.SetFloat("Vertical", lastMoveDirection.y);
        }

        animator.SetFloat("MoveSpeed", move.sqrMagnitude);
    }
}