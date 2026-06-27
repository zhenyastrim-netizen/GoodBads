using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHp = 10;
    public int currentHp;

    public healthbar healthBar;
    public PlayerDash playerDash;

    void Start()
    {
        currentHp = maxHp;

        if (playerDash == null)
            playerDash = GetComponent<PlayerDash>();

        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        if (playerDash != null && playerDash.IsInvincible)
            return;

        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        UpdateHealthBar();

        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHp += amount;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        UpdateHealthBar();
    }

    public void IncreaseMaxHealth(int amount, bool healToFull)
    {
        maxHp += amount;

        if (healToFull)
        {
            currentHp = maxHp;
        }
        else
        {
            currentHp += amount;
            currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        }

        UpdateHealthBar();

        Debug.Log("Максимальное HP увеличено: " + maxHp);
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
            healthBar.SetHealth(currentHp, maxHp);
    }

    void Die()
    {
        Debug.Log("Игрок умер");
    }
}