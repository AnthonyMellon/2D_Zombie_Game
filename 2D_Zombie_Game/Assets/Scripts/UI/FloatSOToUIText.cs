using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatSOToUIText : MonoBehaviour
{
    public floatSO refValue;
    public TMP_Text text;
    [SerializeField] private bool alwaysUpdate;

    private void Update()
    {
        UpdateText();   
    }

    public void UpdateText()
    {
        text.text = refValue.value.ToString();
    }
}
