using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Враг получил урон: " + damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
{
    ExperienceDropper dropper = GetComponent<ExperienceDropper>();

    if (dropper != null)
        dropper.DropExperience();

    Destroy(gameObject);
}
}