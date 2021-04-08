using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class DefenderButton : MonoBehaviour
{
    [SerializeField] Defender defenderPrefab;
    DefenderSpawner defenderSpawner;

    private void Start()
    {
        defenderSpawner = FindObjectOfType<DefenderSpawner>();
        LabelButtonWithCost();
    }

    private void LabelButtonWithCost()
    {
        if (!GetComponent<SpriteRenderer>()) { return; }
        TextMeshProUGUI costText = GetComponentInChildren<TextMeshProUGUI>();
        if(!costText)
        {
            Debug.LogError(name + " has no cost text, please add it.");
        }
        else
        {
            costText.text = defenderPrefab.GetGoldCost().ToString();
        }
    }

    private void OnMouseDown()
    {
        var buttons = FindObjectsOfType<DefenderButton>();
        foreach (DefenderButton button in buttons)
        {
            if (button.GetComponent<SpriteRenderer>())
            {
                button.GetComponent<SpriteRenderer>().color = new Color32(150, 150, 150, 255);
            }
            else
            {
                button.GetComponent<TextMeshProUGUI>().color = new Color32(255, 164, 0, 255);
            }
        }
        if (!GetComponent<SpriteRenderer>())
        {
            defenderSpawner.sellMode = true;            
            GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            defenderSpawner.sellMode = false;            
            GetComponent<SpriteRenderer>().color = Color.white;
            defenderSpawner.SetSelectedDefender(defenderPrefab);
        }
    }
}
