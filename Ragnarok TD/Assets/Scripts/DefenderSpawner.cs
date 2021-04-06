using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    Defender defender;
    private void OnMouseDown()
    {
        AttemptToPlaceDefenderAt(GetSquare());
    }

    public void SetSelectedDefender (Defender defenderToSelect)
    {
        defender = defenderToSelect;
    }

    private void AttemptToPlaceDefenderAt(Vector2 gridPos)
    {
        var GoldDisplay = FindObjectOfType<CurrencyDisplay>();
        if (!defender) { return; };
        int defenderCost = defender.GetGoldCost();
        if (GoldDisplay.HaveEnoughGold(defenderCost))
        {
            SpawnDefender(gridPos);
            GoldDisplay.SpendGold(defenderCost);
        }
    }

    private Vector2 GetSquare()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector2 gridPos = SnapToGrid(worldPos);
        return gridPos;
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);
        return new Vector2(newX, newY);
    }

    private void SpawnDefender(Vector2 roundedPos)
    {

        Defender newDefender = Instantiate(defender, roundedPos, Quaternion.identity) as Defender;
    }
}
