using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<HealthSystem>()?.TakeDamage(damage);
        }
    }
}