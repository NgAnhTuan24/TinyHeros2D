using UnityEngine;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            GameManager.instance.sceneTransition.SetTransitionName(sceneToLoad, sceneTransitionName);
        }   
    }
}
