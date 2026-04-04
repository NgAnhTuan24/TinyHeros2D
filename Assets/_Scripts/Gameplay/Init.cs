using UnityEngine;
using Cinemachine;

public class Init : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] HeartUI heartUI;

    void Awake()
    {
        cam = FindObjectOfType<CinemachineVirtualCamera>();
        heartUI = FindObjectOfType<HeartUI>();
    }

    void Start()
    {
        if (GameManager.instance.checkpointManager != null && string.IsNullOrEmpty(GameManager.instance.checkpointManager.checkpointScene))
        {
            GameManager.instance.checkpointManager.SetCheckpoint(transform.position);
        }

        if (PlayerController.instance == null)
        {
            GameObject player = Instantiate(CharacterSelect.selectedCharacter, transform.position, Quaternion.identity);

            cam.Follow = player.transform;

            player.GetComponent<PlayerHealth>().SetHeartUI(heartUI);

            UIManager.instance.playerManager.RegisterPlayer(player);
        }
        else
        {
            cam.Follow = PlayerController.instance.transform;

            PlayerController.instance.health.SetHeartUI(heartUI);

            UIManager.instance.playerManager.RegisterPlayer(PlayerController.instance.gameObject);
        }
    }
}
