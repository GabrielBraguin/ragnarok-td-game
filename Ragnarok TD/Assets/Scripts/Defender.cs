using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] int goldCost = 100;

    public int GetGoldCost()
    {
        return goldCost;
    }

    public void AddGold(int amount)
    {
        FindObjectOfType<CurrencyDisplay>().AddGold(amount);
    }

    
}
