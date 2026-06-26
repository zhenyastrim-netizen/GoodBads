using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    public int damage = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        HealthSystem currentHp = other.GetComponent<HealthSystem>();

        if (currentHp != null)
        {
            currentHp.TakeDamage(damage);
        }
    }
}