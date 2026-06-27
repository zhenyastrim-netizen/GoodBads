using UnityEngine;

[System.Serializable]
public class StatBlock
{
    public float maxHpPercent;
    public float meleeDamagePercent;     // урон ближнего боя
    public float rangedDamagePercent;    // урон дальнего боя
    public float spellDamagePercent;     // урон заклинаний

    public float attackSpeedPercent;     // скорость атаки
    public float reloadSpeedPercent;     // скорость перезарядки

    public float critChancePercent;      // шанс крита
    public float critDamagePercent;      // крит урон

    public float luckPercent;            // удача
    public float moveSpeedPercent;       // скорость ходьбы

    public void Add(StatBlock other)
    {
        maxHpPercent += other.maxHpPercent;
        meleeDamagePercent += other.meleeDamagePercent;
        rangedDamagePercent += other.rangedDamagePercent;
        spellDamagePercent += other.spellDamagePercent;

        attackSpeedPercent += other.attackSpeedPercent;
        reloadSpeedPercent += other.reloadSpeedPercent;

        critChancePercent += other.critChancePercent;
        critDamagePercent += other.critDamagePercent;

        luckPercent += other.luckPercent;
        moveSpeedPercent += other.moveSpeedPercent;
    }
}