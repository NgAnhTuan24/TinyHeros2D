using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeItemUI : MonoBehaviour
{
    public UpgradeData data;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI valueText;
    public TextMeshProUGUI costText;
    public Button upgradeButton;

    private int currentLevel = 0;

    private void Start()
    {
        RefreshUI();
        upgradeButton.onClick.AddListener(OnUpgradeClicked);
    }

    void RefreshUI()
    {
        if (currentLevel >= data.maxLevel)
        {
            levelText.text = "Lv: MAX";
            valueText.text = "+MAX";
            costText.text = "Cost: MAX";
            upgradeButton.interactable = false;
            return;
        }

        levelText.text = $"Lv: {currentLevel}/{data.maxLevel}";
        valueText.text = $"+{data.values[currentLevel]}";

        int cost = data.prices[currentLevel];
        costText.text = $"Cost: {cost}";

        int currentCoin = GameManager.instance.coinManager.CurrentCoin;

        upgradeButton.interactable = currentCoin >= cost;
    }

    void OnUpgradeClicked()
    {
        if (currentLevel >= data.maxLevel) return;

        int cost = data.prices[currentLevel];

        if (GameManager.instance.coinManager.SpendCoin(cost))
        {
            currentLevel++;
            ApplyUpgrade();
            RefreshUI();
        }
        else
        {
            Debug.Log("Not enough coin");
        }
    }

    void ApplyUpgrade()
    {
        float value = data.values[currentLevel - 1];

        switch (data.type)
        {
            case UpgradeType.Damage:
                PlayerController.instance.hits.AddDamage(value);
                break;

            case UpgradeType.ProjectileDamage:
                GameManager.instance.projectileStatsManager.AddDamage(value);
                break;

            case UpgradeType.ProjectileSpeed:
                GameManager.instance.projectileStatsManager.AddSpeed(value);
                break;
        }
    }
}