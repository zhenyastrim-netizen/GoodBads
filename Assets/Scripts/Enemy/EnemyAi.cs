using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public float moveSpeed = 2f;
    public float detectionRange = 8f;
    public float attackRange = 1.2f;
    public float attackCooldown = 1f;

    private Rigidbody2D rb;
    private float nextAttackTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        Vector2 direction =
            (player.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;
    }

    void Attack()
    {
        if (Time.time < nextAttackTime) return;

        nextAttackTime = Time.time + attackCooldown;

        Debug.Log("Враг атакует игрока");
    }
}