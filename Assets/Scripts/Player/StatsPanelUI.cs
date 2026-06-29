using UnityEngine;
using TMPro;

public class StatsPanelUI : MonoBehaviour
{
    public PlayerStats playerStats;
    public TMP_Text statsText;

    public void UpdateStats()
    {
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats не назначен в StatsPanelUI!");
            return;
        }

        if (statsText == null)
        {
            Debug.LogError("StatsText не назначен в StatsPanelUI!");
            return;
        }

        playerStats.RecalculateStats();

        StatBlock s = playerStats.finalStats;

        statsText.text =
            "СТАТЫ\n\n" +

            "Уровень: " + playerStats.level + "\n\n" +

            "HP: " + FormatPercent(s.maxHpPercent) + "\n" +
            "Ближний урон: " + FormatPercent(s.meleeDamagePercent) + "\n" +
            "Дальний урон: " + FormatPercent(s.rangedDamagePercent) + "\n" +
            "Магический урон: " + FormatPercent(s.spellDamagePercent) + "\n\n" +

            "Скорость атаки: " + FormatPercent(s.attackSpeedPercent) + "\n" +
            "Перезарядка: " + FormatPercent(s.reloadSpeedPercent) + "\n\n" +

            "Шанс крита: " + FormatPercent(s.critChancePercent) + "\n" +
            "Крит урон: " + FormatPercent(s.critDamagePercent) + "\n\n" +

            "Удача: " + FormatPercent(s.luckPercent) + "\n" +
            "Скорость: " + FormatPercent(s.moveSpeedPercent) + "\n\n" +

            "Итог. скорость: " + playerStats.GetMoveSpeed().ToString("0.##");
    }

    private string FormatPercent(float value)
    {
        if (value > 0)
            return "+" + value.ToString("0.#") + "%";

        return value.ToString("0.#") + "%";
    }
}