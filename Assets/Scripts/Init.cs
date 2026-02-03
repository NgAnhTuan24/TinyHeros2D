using UnityEngine;
using Cinemachine;

public class Init : MonoBehaviour
{
    public static Character player;
    public static Transform plTransform;
    public static HeartUI heartUI;

    [SerializeField] private CinemachineVirtualCamera cameraFollow;

    void Start()
    {
        heartUI = FindObjectOfType<HeartUI>();

        GameObject selectedCharacter = CharacterSelect.selectedCharacter;
        GameObject playerObject = Instantiate(selectedCharacter, transform.position, Quaternion.identity);

        playerObject.name = "Player";
        plTransform = playerObject.transform;

        cameraFollow.Follow = plTransform;

        playerObject.GetComponent<PlayerHealth>().SetHeartUI(heartUI);

        switch (selectedCharacter.name)
        {
            case "Pink":
                player = new Pink(playerObject);
                break;
            case "Owlet":
                player = new Owlet(playerObject);
                break;
            case "Dude":
                player = new Dude(playerObject);
                break;
        }
    }
}