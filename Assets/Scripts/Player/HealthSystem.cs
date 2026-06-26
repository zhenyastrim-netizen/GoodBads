using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHp = 100;
    public int currentHp;
    public PlayerDash playerDash;
    void Start()
    {
        currentHp = maxHp;
        
    }

    public void TakeDamage(int damage)
    {
         if (playerDash != null && playerDash.IsInvincible)
        return;

    currentHp -= damage;

    if (currentHp <= 0)
    {
        Die();
    }
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        Debug.Log(currentHp);
        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHp += amount;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
    }

    void Die()
    {
        Debug.Log("Hero died");
        gameObject.SetActive(false);
    }
}