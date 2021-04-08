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

    private void OnMouseDown()
    {
        if (FindObjectOfType<DefenderSpawner>().sellMode)
        {
            AddGold(goldCost/2);            
            GameObject goldCoin = Instantiate(
                Resources.Load("GoldCoin", typeof(GameObject)), gameObject.transform.position, gameObject.transform.rotation)
                as GameObject;
            Destroy(goldCoin, 1f);
            Destroy(gameObject);
        }        
    }

    

}
