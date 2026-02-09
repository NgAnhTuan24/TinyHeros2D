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
        GameObject player = Instantiate(CharacterSelect.selectedCharacter, transform.position, Quaternion.identity);

        cam.Follow = player.transform;

        player.GetComponent<PlayerHealth>().SetHeartUI(heartUI);
    }
}
