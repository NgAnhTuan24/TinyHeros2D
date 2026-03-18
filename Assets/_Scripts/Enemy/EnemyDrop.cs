using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [Header("Drop Config")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int minCoin = 1;
    [SerializeField] private int maxCoin = 3;

    [Header("Scatter")]
    [SerializeField] private float force = 3f;

    public void Drop()
    {
        int amount = Random.Range(minCoin, maxCoin + 1);

        for (int i = 0; i < amount; i++)
        {
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 randomDir = new Vector2(
                    Random.Range(-1f, 1f),
                    Random.Range(0.5f, 1f)
                ).normalized;

                rb.AddForce(randomDir * force, ForceMode2D.Impulse);
            }
        }
    }
}
