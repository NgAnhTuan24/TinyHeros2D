using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Data/DialogueText")]
public class DialogueData : ScriptableObject
{
    [TextArea]
    public string[] lines;
}
