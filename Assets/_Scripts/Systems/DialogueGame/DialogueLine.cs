using UnityEngine;

public enum SpeakerType
{
    NPC,
    Player
}

[System.Serializable]
public class DialogueLine
{
    public SpeakerType speaker;

    public Sprite icon;
    public string name;
    [TextArea] public string text;
}