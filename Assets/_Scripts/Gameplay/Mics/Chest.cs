using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private bool isOpened = false;
    [SerializeField] private int coinGame = 10;

    [Header("Visual")]
    [SerializeField] private Sprite closedChest;
    [SerializeField] private Sprite openedChest;

    [SerializeField] private AudioClip collectSFX;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = closedChest;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpened) return;

        if (other.CompareTag("Player"))
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        isOpened = true;

        sr.sprite = openedChest;

        GameManager.instance.coinManager.AddCoin(coinGame);

        AudioManager.instance.PlaySFX(collectSFX);

        Debug.Log("Chest opened!");

        // TODO:
        // - Spawn item
        // - Play sound
        // - Animation
    }
}