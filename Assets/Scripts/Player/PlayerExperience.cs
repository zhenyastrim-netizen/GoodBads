using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int level = 1;

    public int currentExp = 0;
    public int expToNextLevel = 100;

    public int hpPerLevel = 2;
    public bool healToFullOnLevelUp = true;

    public ExpManager experienceBar;
    public HealthSystem playerHealth;

    void Start()
    {
        if (playerHealth == null)
            playerHealth = GetComponent<HealthSystem>();

        UpdateExpBar();
    }

    public void AddExperience(int amount)
    {
        currentExp += amount;

        while (currentExp >= expToNextLevel)
        {
            currentExp -= expToNextLevel;
            LevelUp();
        }

        UpdateExpBar();
    }

    void LevelUp()
    {
        level++;

        if (playerHealth != null)
        {
            playerHealth.IncreaseMaxHealth(hpPerLevel, healToFullOnLevelUp);
        }

        expToNextLevel = Mathf.RoundToInt(expToNextLevel * 1.25f);

        Debug.Log("Новый уровень: " + level);
    }

    void UpdateExpBar()
    {
        if (experienceBar != null)
            experienceBar.SetExperience(currentExp, expToNextLevel, level);
    }
}