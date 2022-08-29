using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kiosk_Entity : MonoBehaviour
{
    public Kiosk_SO kioskListings;
    public Canvas kioskUI;


    private void OnMouseDown()
    {
        Instantiate(kioskUI);
    }
}
