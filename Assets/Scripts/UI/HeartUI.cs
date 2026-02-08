using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    public GameObject heartPrefab;
    public Sprite heartFull;
    public Sprite heartEmpty;

    private List<Image> hearts = new List<Image>();

    public void Init(int maxHp)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        hearts.Clear();

        for (int i = 0; i < maxHp; i++)
        {
            GameObject heart = Instantiate(heartPrefab, transform);
            Image img = heart.GetComponent<Image>();
            img.sprite = heartFull;
            hearts.Add(img);
        }
    }

    public void UpdateHearts(int currentHp)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].sprite = i < currentHp ? heartFull : heartEmpty;
        }
    }
}
