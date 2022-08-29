using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KioskListing : MonoBehaviour
{
    public floatSO playerMoney;
    public KioskItem_SO listedItem;
    public WeaponEvent purchaseEvent;
    public void AttemptPurchase()
    {
        Debug.Log($"Attempted purchase of ${listedItem.cost}, player has ${playerMoney.value}");
        if (playerMoney.value >= listedItem.cost)
        {
            playerMoney.value -= listedItem.cost;
            Debug.Log($"Purchase Succeded, player now has ${playerMoney.value}");
            purchaseEvent.Raise(listedItem.weapon);
        }
            
        else Debug.Log("Purchase Failed");
    }
}
