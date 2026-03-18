using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDialogueUI : MonoBehaviour
{
    public GameObject panel;

    public Image icon1;
    public Image icon2;

    public TextMeshProUGUI text;

    public void ShowDialogue(Sprite iconLeft, Sprite iconRight, string message)
    {
        panel.SetActive(true);
        text.text = message;

        if(iconLeft != null)
        {
            icon1.gameObject.SetActive(true);
            icon1.sprite = iconLeft;
        }
        else
        {
            icon1.gameObject.SetActive(false);
        }

        if (iconRight != null)
        {
            icon2.gameObject.SetActive(true);
            icon2.sprite = iconRight;
        }
        else
        {
            icon2.gameObject.SetActive(false);
        }
    }

    public void HideDialogue()
    {
        panel.SetActive(false);
    }
}
