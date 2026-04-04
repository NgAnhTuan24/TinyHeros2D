using UnityEngine;

public class UICloseButton : MonoBehaviour
{
    [SerializeField] private UIType uiType;

    public void Close()
    {
        UIManager.instance.CloseUI(uiType);
    }
}