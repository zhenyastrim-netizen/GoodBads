using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Melee,
    Ranged,
    Spell
}

public class PlayerStats : MonoBehaviour
{
    public PlayerStatsConfig config;

    public int level = 1;

    public StatBlock finalStats = new StatBlock();

    private List<StatBlock> itemBonuses = new List<StatBlock>();

    void Start()
    {
        RecalculateStats();
    }

    public void AddItemStats(StatBlock itemStats)
    {
        itemBonuses.Add(itemStats);
        RecalculateStats();
    }

    public void RemoveItemStats(StatBlock itemStats)
    {
        itemBonuses.Remove(itemStats);
        RecalculateStats();
    }

    public void LevelUp()
    {
        level++;
        RecalculateStats();
    }

    public void RecalculateStats()
    {
        finalStats = new StatBlock();

        // базовые статы
        finalStats.Add(config.baseStats);

        // бонусы за уровень
        for (int i = 1; i < level; i++)
        {
            finalStats.Add(config.statsPerLevel);
        }

        // бонусы от предметов
        foreach (StatBlock itemBonus in itemBonuses)
        {
            finalStats.Add(itemBonus);
        }
    }

    public float GetDamageMultiplier(DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.Melee:
                return 1f + finalStats.meleeDamagePercent / 100f;

            case DamageType.Ranged:
                return 1f + finalStats.rangedDamagePercent / 100f;

            case DamageType.Spell:
                return 1f + finalStats.spellDamagePercent / 100f;
        }

        return 1f;
    }

    public float GetAttackSpeedMultiplier()
    {
        return 1f + finalStats.attackSpeedPercent / 100f;
    }

    public float GetReloadSpeedMultiplier()
    {
        return 1f + finalStats.reloadSpeedPercent / 100f;
    }

    public float GetMoveSpeed()
    {
        return config.baseMoveSpeed * (1f + finalStats.moveSpeedPercent / 100f);
    }

    public bool RollCrit()
    {
        float chance = Mathf.Clamp(finalStats.critChancePercent, 0f, 100f);
        return Random.Range(0f, 100f) <= chance;
    }

    public float GetCritMultiplier()
    {
        // 150% = x1.5
        return finalStats.critDamagePercent / 100f;
    }
}