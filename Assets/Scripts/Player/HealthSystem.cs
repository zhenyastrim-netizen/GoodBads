using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHp = 100;
    public int currentHp;
    public PlayerDash playerDash;
    public healthbar healthBar;
    void Start()
    {
        currentHp = maxHp;
          currentHp = maxHp;

        if (playerDash == null)
            playerDash = GetComponent<PlayerDash>();

        if (healthBar != null)
            healthBar.SetHealth(currentHp, maxHp);
        
    }

    public void TakeDamage(int damage)
    {
         if (playerDash != null && playerDash.IsInvincible)
        return;

    currentHp -= damage;
    if (healthBar != null)
            healthBar.SetHealth(currentHp, maxHp);
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
        if (healthBar != null)
            healthBar.SetHealth(currentHp, maxHp);
    }

    void Die()
    {
        Debug.Log("Hero died");
        gameObject.SetActive(false);
    }
}