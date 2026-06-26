using UnityEngine;

[CreateAssetMenu(menuName = "Player/Stats Config")]
public class PlayerStatsConfig : ScriptableObject
{
    public float baseMoveSpeed = 3f;

    public StatBlock baseStats;
    public StatBlock statsPerLevel;
}