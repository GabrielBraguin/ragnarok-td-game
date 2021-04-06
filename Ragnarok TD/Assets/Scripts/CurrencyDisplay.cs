using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CurrencyDisplay : MonoBehaviour
{
    int gold = 100;
    TextMeshProUGUI goldDisplayText;
    void Start()
    {
        goldDisplayText = GetComponent<TextMeshProUGUI>();
        SetDifficultyInitialGoldModifier();
        UpdateDisplay();
    }

    private void SetDifficultyInitialGoldModifier()
    {
        switch (PlayerPrefsController.GetDifficulty())
        {
            case 1:
                gold = 1000;
                break;
            case 2:
                gold = 600;
                break;
            case 3:
                gold = 300;
                break;
            default:
                gold = 600;
                break;
        }
    }

    private void UpdateDisplay()
    {
        goldDisplayText.text = gold.ToString();
    }
    
    public bool HaveEnoughGold(int amount)
    {
        return gold >= amount;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateDisplay();
    }

    public void SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            UpdateDisplay();
        }        
    }
}

