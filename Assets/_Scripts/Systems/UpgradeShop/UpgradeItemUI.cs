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

        GameManager.instance.coinManager.OnCoinChanged += OnCoinChanged;
    }

    void LoadLevel()
    {
        var dict = GameManager.instance.upgradeLevels;

        if (dict.ContainsKey(data.type))
            currentLevel = dict[data.type];
        else
            currentLevel = 0;
    }

    public void RefreshUI()
    {
        bool isUnlocked = GameManager.instance.IsUpgradeUnlocked(data.type);
        if (!isUnlocked)
        {
            levelText.text = "Locked";
            valueText.text = "???";
            costText.text = "Unlock via tutorial";
            upgradeButton.interactable = false;
            return;
        }

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
        if (!GameManager.instance.IsUpgradeUnlocked(data.type))
            return;

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

    private void OnCoinChanged(int coin)
    {
        RefreshUI();
    }
}