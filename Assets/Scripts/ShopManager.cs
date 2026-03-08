using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Character Data")]
    public Sprite[] characterSprites;
    public string[] characterNames;
    public int[] characterPrices;

    private int currentIndex = 0;

    [Header("UI")]
    public Image characterImage;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI characterPrice;

    [Header("Coin")]
    public TextMeshProUGUI coinText;

    private int coin;

    void Start()
    {
        ShopUI();
    }

    void ShopUI()
    {
        characterImage.sprite = characterSprites[currentIndex];
        characterName.text = characterNames[currentIndex];
        characterPrice.text = characterPrices[currentIndex].ToString();

        coinText.text = coin.ToString();
    }

    public void NextButton()
    {
        currentIndex++;

        if (currentIndex >= characterSprites.Length)
            currentIndex = 0;

        ShopUI();
    }

    public void PrevCharacter()
    {
        currentIndex--;

        if (currentIndex < 0)
            currentIndex = characterSprites.Length - 1;

        ShopUI();
    }
}
