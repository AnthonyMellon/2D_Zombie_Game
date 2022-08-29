using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Kiosk_UI : MonoBehaviour
{
    public Kiosk_SO kioskItems;
    public GameObject kioskListing;

    [SerializeField]
    [Tooltip("padding of 1 is equal in size to one listing")]
    private float listingPadding;
    private float truePadding;

    private void OnEnable()
    {
        truePadding = listingPadding * Screen.height * kioskListing.transform.localScale.y;

        for (int i = 0; i < kioskItems.Inventory.Count; i++)
        {
            GameObject listing = Instantiate(kioskListing, transform);
            float listingHeight = listing.transform.lossyScale.y * Screen.height;

            listing.transform.localPosition = new Vector3(0, (listingHeight + truePadding) * i, 0);
            listing.transform.Find("ItemName").GetComponent<TMP_Text>().text = kioskItems.Inventory[i].weapon.name;
            listing.transform.Find("BuyButton").transform.Find("Text (TMP)").GetComponent<TMP_Text>().text = kioskItems.Inventory[i].cost.ToString();
            listing.transform.GetComponent<KioskListing>().listedItem = kioskItems.Inventory[i];
        }
    }

    public void killSelf()
    {
        Destroy(gameObject);
    }
}
