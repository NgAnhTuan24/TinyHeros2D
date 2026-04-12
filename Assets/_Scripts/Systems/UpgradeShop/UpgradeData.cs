using UnityEngine;

public enum UpgradeType
{
    Damage,
    ProjectileDamage,
    ProjectileSpeed
}

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Data/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public UpgradeType type;
    public int maxLevel;

    public int[] prices;
    public float[] values;
}
