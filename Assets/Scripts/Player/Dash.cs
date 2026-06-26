using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    public Rigidbody2D rb;
    public Player player;

    public float dashSpeed = 15f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 0.6f;

    public bool IsDashing { get; private set; }
    public bool IsInvincible { get; private set; }

    private bool canDash = true;

    void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (player == null)
            player = GetComponent<Player>();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        IsDashing = true;
        IsInvincible = true;

        Vector2 dashDirection;

        // Если игрок сейчас нажимает WASD
        if (player.MoveInput.sqrMagnitude > 0.01f)
        {
            dashDirection = player.MoveInput.normalized;
        }
        // Если не нажимает — dash в последнюю сторону движения
        else
        {
            dashDirection = player.LastMoveDirection;
        }

        rb.linearVelocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb.linearVelocity = Vector2.zero;

        IsDashing = false;
        IsInvincible = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
}