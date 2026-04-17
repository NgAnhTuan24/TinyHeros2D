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

    private int currentLevel;

    private void Start()
    {
        LoadLevel();
        RefreshUI();
        upgradeButton.onClick.AddListener(OnUpgradeClicked);
    }

    void LoadLevel()
    {
        var dict = GameManager.instance.upgradeLevels;

        if (dict.ContainsKey(data.type))
            currentLevel = dict[data.type];
        else
            currentLevel = 0;
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

            GameManager.instance.upgradeLevels[data.type] = currentLevel;

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
        ApplyUpgradeAtLevel(currentLevel - 1);
    }

    void ApplyUpgradeAtLevel(int levelIndex)
    {
        float value = data.values[levelIndex];

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