using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    void Start()
    {
        GameManager.instance.coinManager.OnCoinChanged += UpdateUI;

        UpdateUI(GameManager.instance.coinManager.CurrentCoin);
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
