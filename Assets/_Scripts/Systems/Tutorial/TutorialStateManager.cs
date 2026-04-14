using UnityEngine;

public class TutorialStateManager : MonoBehaviour
{
    public bool jumpUnlocked;
    public bool attackUnlocked;
    public bool throwUnlocked;

    public void LoadFromData(GameData data)
    {
        jumpUnlocked = data.jumpUnlocked;
        attackUnlocked = data.attackUnlocked;
        throwUnlocked = data.throwUnlocked;
    }

    public void SaveToData(GameData data)
    {
        data.jumpUnlocked = jumpUnlocked;
        data.attackUnlocked = attackUnlocked;
        data.throwUnlocked = throwUnlocked;
    }
}
