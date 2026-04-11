using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1;
    [SerializeField] private AudioClip collectCoinSFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFX(collectCoinSFX);

            GameManager.instance.coinManager.AddCoin(coinValue);

            Destroy(gameObject);
        }
    }
}
