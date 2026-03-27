using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;

    private void Start()
    {
        if (transitionName == GameManager.instance.sceneTransition.SceneTransitionName)
        {
            PlayerController.instance.transform.position = this.transform.position;
            GameManager.instance.cameraController.SetPlayerCameraFollow();

            UIManager.instance.SetUIActive(true);
            PlayerController.instance.movement.UnlockPlayer();
        }
    }
}
