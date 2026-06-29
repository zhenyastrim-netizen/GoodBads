using UnityEngine;
using System.Collections.Generic;

public class AttackHitbox : MonoBehaviour
{
    public int damage = 1;

    private HashSet<HealthSystem> damagedEnemies = new HashSet<HealthSystem>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        HealthSystem health = other.GetComponent<HealthSystem>();

        if (health == null)
            return;

        if (damagedEnemies.Contains(health))
            return;

        damagedEnemies.Add(health);
        health.TakeDamage(damage);
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}