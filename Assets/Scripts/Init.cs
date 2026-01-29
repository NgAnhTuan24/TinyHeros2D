using UnityEngine;

public class Init : MonoBehaviour
{
    public static Character player;
    public static Transform plTransform;

    void Start()
    {
        GameObject selectedCharacter = CharacterSelect.selectedCharacter;
        GameObject playerObject = Instantiate(selectedCharacter, transform.position, Quaternion.identity);
        playerObject.name = "Player";
        plTransform = playerObject.transform;

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