using UnityEngine;

public class AreaExit : MonoBehaviour, IInteractable
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;

    [Header("Config")]
    [SerializeField] private bool requireButtonPress;

    private bool playerInZone;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            playerInZone = true;

            if (!requireButtonPress)
            {
                TriggerTransition();
            }
            else
            {
                UIManager.instance.controlUIManager.ShowInteractButton(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            playerInZone = false;

            if (requireButtonPress)
            {
                UIManager.instance.controlUIManager.HideInteractButton(this);
            }
        }
    }

    public void Interact()
    {
        if (playerInZone)
        {
            TriggerTransition();
        }
    }

    private void TriggerTransition()
    {
        GameManager.instance.sceneTransition.SetTransitionName(sceneToLoad, sceneTransitionName);
    }
}
