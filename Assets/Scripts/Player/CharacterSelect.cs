using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] characters;
    public TextMeshProUGUI characterName;
    public GameObject[] characterPrefabs;

    public static GameObject selectedCharacter;

    private Vector3[] positions;

    void Start()
    {
        positions = new Vector3[characters.Length];
        for (int i = 0; i < characters.Length; i++)
        {
            positions[i] = characters[i].transform.position;
        }

        SelectCharater();
    }

    public void OnStartBtnClick()
    {
        SceneManager.LoadScene(2);
    }

    public void OnPrevBtnClick()
    {
        RotateLeft();
        SelectCharater();
    }

    public void OnNextBtnClick()
    {
        RotateRight();
        SelectCharater();
    }

    private void RotateRight()
    {
        GameObject lastChar = characters[characters.Length - 1];
        GameObject lastPrefab = characterPrefabs[characterPrefabs.Length - 1];

        for (int i = characters.Length - 1; i > 0; i--)
        {
            characters[i] = characters[i - 1];
            characterPrefabs[i] = characterPrefabs[i - 1];
        }

        characters[0] = lastChar;
        characterPrefabs[0] = lastPrefab;
    }

    private void RotateLeft()
    {
        GameObject firstChar = characters[0];
        GameObject firstPrefab = characterPrefabs[0];

        for (int i = 0; i < characters.Length - 1; i++)
        {
            characters[i] = characters[i + 1];
            characterPrefabs[i] = characterPrefabs[i + 1];
        }

        characters[characters.Length - 1] = firstChar;
        characterPrefabs[characterPrefabs.Length - 1] = firstPrefab;
    }

    private void SelectCharater()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].transform.position = positions[i];

            var sr = characters[i].GetComponent<SpriteRenderer>();
            var anim = characters[i].GetComponent<Animator>();

            if (i == 0)
            {
                sr.color = Color.white;
                anim.enabled = true;

                selectedCharacter = characterPrefabs[0];
                characterName.text = selectedCharacter.name;
            }
            else
            {
                sr.color = Color.black;
                anim.enabled = false;
            }
        }
    }
}
