using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        HealthSystem health = other.GetComponent<HealthSystem>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}