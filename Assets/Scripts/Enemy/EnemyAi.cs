using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public float moveSpeed = 2f;
    public float detectionRange = 8f;
    public float attackRange = 1.2f;
    public float attackCooldown = 1f;

    [Header("Attack")]
    public GameObject hitboxPrefab;
    public float attackDistance = 0.7f;
    public float hitboxDuration = 0.15f;
    public int damage = 1;

    public Animator animator;

    private Rigidbody2D rb;
    private float nextAttackTime;

    private Vector2 lastAttackDirection = Vector2.down;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
                player = playerObject.transform;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;
            Attack();
        }
        else if (distance <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;

        if (animator != null)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("MoveSpeed", 1f);
        }
    }

    void Attack()
    {
        if (Time.time < nextAttackTime) return;

        nextAttackTime = Time.time + attackCooldown;

        Vector2 direction = (player.position - transform.position).normalized;
        lastAttackDirection = direction;

        if (animator != null)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("MoveSpeed", 0f);
            animator.SetTrigger("Attack");
        }

        SpawnHitbox();

        Debug.Log("Враг атакует игрока");
    }

    public void SpawnHitbox()
    {
        if (hitboxPrefab == null)
        {
            Debug.LogError("Hitbox Prefab у врага не назначен!");
            return;
        }

        Vector3 spawnPos = transform.position + (Vector3)lastAttackDirection * attackDistance;

        float angle = Mathf.Atan2(lastAttackDirection.y, lastAttackDirection.x) * Mathf.Rad2Deg;

        GameObject hitbox = Instantiate(
            hitboxPrefab,
            spawnPos,
            Quaternion.Euler(0, 0, angle)
        );

        EnemyAttackHitbox attackHitbox = hitbox.GetComponent<EnemyAttackHitbox>();

        if (attackHitbox != null)
            attackHitbox.damage = damage;
        else
            Debug.LogError("На hitboxPrefab нет EnemyAttackHitbox!");

        Destroy(hitbox, hitboxDuration);
    }
}