using UnityEngine;

public class Init : MonoBehaviour
{
    public static Character player;

    void Start()
    {
        GameObject selectedCharacter = CharacterSelect.selectedCharacter;
        GameObject playerObject = Instantiate(selectedCharacter, transform.position, Quaternion.identity);
        playerObject.name = "Player";

        switch (selectedCharacter.name)
        {
            case "Pink":
                player = new Pink(playerObject);
                break;
        }
    }
}
