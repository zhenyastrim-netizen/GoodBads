using UnityEngine;

public class AttackProjection : MonoBehaviour
{
    public Animator animator;

    public GameObject hitboxPrefab;
    public Transform attackPoint;

    public float hitboxDuration = 0.1f;
    public float lifeTime = 0.6f;

    private int damage;
    private Vector2 attackDirection;

    void Awake()
    {
    

    if (animator == null)
        animator = GetComponent<Animator>();

    if (attackPoint == null)
        attackPoint = transform.Find("AttackPoint");

    if (attackPoint == null)
        Debug.LogError("AttackPoint не найден! Он должен быть дочерним объектом AttackGhost.");

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void Setup(int comboIndex, int newDamage, Vector2 direction)
    {
        damage = newDamage;
        attackDirection = direction.normalized;

        Debug.Log("AttackProjection Setup: Attack" + comboIndex);

        animator.SetFloat("Horizontal", attackDirection.x);
        animator.SetFloat("Vertical", attackDirection.y);

        animator.SetTrigger("Attack" + comboIndex);

        Destroy(gameObject, lifeTime);
    }

    public void SpawnHitbox()
    {
        Debug.Log("SpawnHitbox вызвался!");

        if (hitboxPrefab == null)
        {
            Debug.LogError("Hitbox Prefab не назначен!");
            return;
        }

        if (attackPoint == null)
        {
            Debug.LogError("Attack Point не назначен!");
            return;
        }

        float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;

        GameObject hitbox = Instantiate(
            hitboxPrefab,
            attackPoint.position,
            Quaternion.Euler(0, 0, angle)
        );

        AttackHitbox attackHitbox = hitbox.GetComponent<AttackHitbox>();

        if (attackHitbox != null)
            attackHitbox.damage = damage;
        else
            Debug.LogError("На hitboxPrefab нет скрипта AttackHitbox!");

        Destroy(hitbox, hitboxDuration);
    }
}