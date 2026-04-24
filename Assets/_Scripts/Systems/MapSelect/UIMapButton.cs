using UnityEngine;

public class UIMapButton : MonoBehaviour
{
    [SerializeField] private GameObject portalPrefab;

    public void OnClick()
    {
        MapSpawner.instance.Spawn(portalPrefab);

        UIManager.instance.CloseUI(UIType.SelectMap);
    }
}