using UnityEngine;

public class ProjectileStatsManager : MonoBehaviour
{
    public float bonusDamage = 0f;
    public float bonusSpeed = 0f;

    public void AddDamage(float value)
    {
        bonusDamage += value;
    }

    public void AddSpeed(float value)
    {
        bonusSpeed += value;
    }
}
