using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private enum SurvivorSelected
    {
        none,
        player
    };
    private SurvivorSelected survivorSelected = SurvivorSelected.none;

    private int playerCurrency;
    public int playerLevel = 1;
    private int upgradeCost = 10;
    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI upgradeCostText;
    public TextMeshProUGUI playerLevelText;
    public Button playerUpgradeButton;


    private void Start()
    {
        playerCurrency = 0;
        UpdateCurrencyText();
        UpdateUpgradeCostText();
        UpdatePlayerLevelText();
        CheckPlayerUpgradeCurrency();
    }

    public void AddCurrency(int currencyToAdd)
    {
        playerCurrency += currencyToAdd;
        CheckPlayerUpgradeCurrency();
        UpdateCurrencyText();
    }

    private void UpdateCurrencyText()
    {
        if (currencyText == null)
        {
            Debug.LogError("Currency text not found by game manager.");
        }
        else
        {
            currencyText.text = playerCurrency.ToString();
        }
    }

    private void UpdatePlayerLevelText()
    {
        playerLevelText.text = playerLevel.ToString();
    }

    private void UpdateUpgradeCostText()
    {
        if (upgradeCostText == null)
        {
            Debug.LogError("Upgrade cost text not found by game manager.");
        }
        else
        {
            upgradeCostText.text = upgradeCost.ToString();
        }
    }

    public void UpgradePlayer()
    {
        if (CheckPlayerUpgradeCurrency())
        {
            playerCurrency -= upgradeCost;
            playerLevel++;
            upgradeCost = (int)(1.5f * upgradeCost);
            UpdateCurrencyText();
            UpdatePlayerLevelText();
            UpdateUpgradeCostText();
            CheckPlayerUpgradeCurrency();
        }
    }

    private bool CheckPlayerUpgradeCurrency()
    {
        if (survivorSelected == SurvivorSelected.player && playerCurrency >= upgradeCost)
        {
            TogglePlayerUpgradeButton(true);
            return true;
        }
        else
        {
            TogglePlayerUpgradeButton(false);
            return false;
        }
    }

    private void TogglePlayerUpgradeButton(bool toggle)
    {
        ColorBlock colors = playerUpgradeButton.colors;
        if (toggle)
        {
            colors.normalColor = Color.white;
            playerUpgradeButton.interactable = true;
        }
        else
        {
            colors.normalColor = Color.gray;
            playerUpgradeButton.interactable = false;
        }
        playerUpgradeButton.colors = colors;
    }

    public void SelectPlayer()
    {
        survivorSelected = SurvivorSelected.player;
        CheckPlayerUpgradeCurrency();
    }

    public void ClearSelect()
    {
        survivorSelected = SurvivorSelected.none;
        playerUpgradeButton.enabled = false;
    }
}
