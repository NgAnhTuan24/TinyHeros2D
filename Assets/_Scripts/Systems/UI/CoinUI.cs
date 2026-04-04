using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    void Start()
    {
        UpdateUI(GameManager.instance.coinManager.CurrentCoin);

        GameManager.instance.coinManager.OnCoinChanged += UpdateUI;
    }

    private void UpdateUI(int coin)
    {
        coinText.text = coin.ToString();
    }

    private void OnDestroy()
    {
        GameManager.instance.coinManager.OnCoinChanged -= UpdateUI;
    }
}
